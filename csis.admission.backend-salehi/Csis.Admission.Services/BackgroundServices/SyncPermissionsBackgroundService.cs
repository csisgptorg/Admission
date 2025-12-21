/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Enums;
using Csis.Authorization.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Services.BackgroundServices;

/// <summary>
/// سرویس همگام‌سازی دسترسی‌ها
/// </summary>
/// <param name="csisAuthorizationService"></param>
/// <param name="cache"></param>
/// <param name="dateTimeService"></param>
/// <param name="logger"></param>
internal sealed class SyncPermissionsBackgroundService(
    ICsisAuthorizationService csisAuthorizationService,
    IDistributedCacheService cache,
    IDateTimeService dateTimeService,
    ILogger<SyncPermissionsBackgroundService> logger) : IHostedLifecycleService
{
    private const string CacheKey = "PermissionsLastSync";

    public Task StartAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }

    public async Task StartedAsync(CancellationToken cancellationToken) {
        var lastSync = await cache.GetAsync<DateTime?>(CacheKey, cancellationToken);

        if ( !lastSync.HasValue || lastSync.Value.AddMinutes(30) <= dateTimeService.Now ) {
            var result = await csisAuthorizationService.SyncAllPermissionsAsync<PermissionsEnum>();
            if ( result.Succeeded ) {
                logger.LogInformation("Permissions synchronization completed successfully.");
                await cache.SetAsync(CacheKey, dateTimeService.Now, cancellationToken);
            } else {
                logger.LogError("Permissions synchronization failed: {@result}", result);
            }
        }
    }

    public Task StartingAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}
