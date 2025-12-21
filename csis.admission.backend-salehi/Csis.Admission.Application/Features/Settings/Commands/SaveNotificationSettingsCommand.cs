/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces.Settings;
using Csis.Admission.Domain.Settings;

namespace Csis.Admission.Application.Features.Settings.Commands;

/// <summary>
/// ذخیره تنظیمات نوتیفیکیشن
/// </summary>
/// <param name="NotificationSettings">مقدار تنظیمات</param>
public sealed record SaveNotificationSettingsCommand(NotificationSettings NotificationSettings) : IRequest;

internal sealed class SaveNotificationSettingsCommandHandler(ISettingsService settingsService, ILogger<SaveNotificationSettingsCommandHandler> logger) : IRequestHandler<SaveNotificationSettingsCommand>
{
    public async Task Handle(SaveNotificationSettingsCommand request, CancellationToken cancellationToken) {
        var currentSettings = await settingsService.GetAsync<NotificationSettings>();

        logger.LogInformation("Changing notification settings from {@currentValue} to {@newValue}", currentSettings, request.NotificationSettings);

        await settingsService.SaveAsync(request.NotificationSettings);
    }
}
