/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Settings;
using Csis.Admission.Domain.Settings;
using Csis.Notification;
using Csis.Utilities.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Csis.Admission.Services.BackgroundServices;

/// <summary>
/// سرویس ارسال نوتیفیکیشن‌های در صف ارسال
/// </summary>
/// <param name="logger"></param>
/// <param name="serviceProvider"></param>
internal sealed class SendNotificationBackgroundService(
    ILogger<SendNotificationBackgroundService> logger,
    IServiceProvider serviceProvider) : BackgroundService
{
    private const string ServiceTitle = "Send Notification";

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        try {
            await RunService(stoppingToken);
            using PeriodicTimer timer = new(TimeSpan.FromMinutes(2));

            while ( await timer.WaitForNextTickAsync(stoppingToken) ) {
                await RunService(stoppingToken);
            }
        } catch ( OperationCanceledException ) {
            logger.LogInformation(Events.SendNotificationBackgroundService, "Send notification background service is stopping.");
        } catch ( Exception ex ) {
            logger.LogError(Events.SendNotificationBackgroundService, ex, "Send notification background service is failed.");
        }
    }

    private async Task RunService(CancellationToken stoppingToken) {
        logger.LogInformation(Events.SendNotificationBackgroundService, "Send notification background service is running.");

        var reason = await DoWorkAsync(stoppingToken);

        logger.LogInformation(Events.SendNotificationBackgroundService, "Send notification background service finished. Reason: {reason}", reason);
    }

    private async Task<string> DoWorkAsync(CancellationToken cancellationToken) {
        await Task.Delay(Random.Shared.Next(1, 15) * 1000, cancellationToken); // Avoid duplicate instances running

        using var scope = serviceProvider.CreateScope();
        var reportRepo = scope.ServiceProvider.GetRequiredService<IBackgroundServiceReportRepository>();
        var dateTimeService = scope.ServiceProvider.GetRequiredService<IDateTimeService>();
        var settingsService = scope.ServiceProvider.GetRequiredService<ISettingsService>();

        await reportRepo.ForceStopLongRunningServicesAsync(TimeSpan.FromMinutes(30), ServiceTitle, cancellationToken);

        if ( await reportRepo.IsRunningAsync(ServiceTitle, cancellationToken) ) {
            return "RUNNING";
        }

        var notificationSettings = (await settingsService.GetAsync<NotificationSettings>()).Value;
        if ( !notificationSettings.EnableNotificationSenderService ) {
            return "NOT_ENABLED";
        }

        if ( notificationSettings.NotificationSenderServiceBatchSize is < 10 or > 500 ) {
            return $"INVALID_BATCH_SIZE: {notificationSettings.NotificationSenderServiceBatchSize}";
        }

        if ( notificationSettings.NotificationSenderServiceIntervalInSeconds is < 30 or > 1800 ) {
            return $"INVALID_INTERVAL: {notificationSettings.NotificationSenderServiceIntervalInSeconds}";
        }

        if ( notificationSettings.MaxTryPerNotification is < 3 or > 50 ) {
            return $"INVALID_MAX_TRY: {notificationSettings.MaxTryPerNotification}";
        }

        var notificationHistoryRepo = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
        if ( !await notificationHistoryRepo.HasPendingNotificationAsync(cancellationToken) ) {
            return "NO_PENDING_NOTIFICATION";
        }

        var lastDate = await reportRepo.GetLastFinishedDateAsync(ServiceTitle, cancellationToken);
        if ( !lastDate.HasValue || lastDate.Value.AddSeconds(notificationSettings.NotificationSenderServiceIntervalInSeconds) < dateTimeService.Now ) {
            var notificationService = scope.ServiceProvider.GetRequiredService<ICsisNotificationService>();
            var report = await reportRepo.StartAsync(ServiceTitle, cancellationToken);
            var sw = Stopwatch.StartNew();

            try {
                var pendingNotifications = await notificationHistoryRepo.GetPendingNotificationsAsync(notificationSettings.NotificationSenderServiceBatchSize, cancellationToken);
                var successCount = 0;
                var failedCount = 0;
                var cancelledCount = 0;

                foreach ( var notification in pendingNotifications ) {
                    try {
                        var messageId = notification.Codm.HasValue ?
                            await notificationService.SendMessageToStudent(new SendMessageToStudent(
                                notification.Message,
                                [notification.Codm.Value],
                                [.. notification.DeliveryChannels.Select(x => (DeliveryChannelEnum) x)]), CancellationToken.None) :
                            await notificationService.SendMessageToEmployee(new SendMessageToEmployee(
                                notification.Message,
                                [notification.PersonnelId.Value],
                                [.. notification.DeliveryChannels.Select(x => (DeliveryChannelEnum) x)]), CancellationToken.None);

                        notification.SendSuccessfully(dateTimeService.Now, messageId);
                        successCount++;

                    } catch ( Exception ex ) {
                        logger.LogError(ex, "Error sending notification to codm {codm}", notification.Codm);

                        notification.SendFailed(dateTimeService.Now);
                        failedCount++;
                        if ( notification.TriesCount > notificationSettings.MaxTryPerNotification ) {
                            notification.Cancel();
                            cancelledCount++;
                            logger.LogWarning("Notification with id {notificationId} cancelled", notification.Id);
                        }
                    }

                    await Task.Delay(50, cancellationToken);
                }

                await notificationHistoryRepo.UpdateAsync(pendingNotifications, cancellationToken: CancellationToken.None);

                sw.Stop();

                await reportRepo.FinishAsync(report.Id, $"Processed {pendingNotifications.Count} items. success: {successCount} failed: {failedCount} cancelled: {cancelledCount}. Settings: {notificationSettings.ToJson()}", sw.ElapsedMilliseconds, cancellationToken);
            } catch ( Exception ex ) {
                sw.Stop();
                logger.LogError(ex, "Error running background service");
                await reportRepo.FinishWithErrorAsync(report.Id, $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", sw.ElapsedMilliseconds, cancellationToken);
                return $"ERROR: {ex.Message}";
            } finally {
                sw.Stop();
            }

            return $"FINISHED - BATCH: {notificationSettings.NotificationSenderServiceBatchSize}";
        }

        return "NOT_TIME";
    }
}
