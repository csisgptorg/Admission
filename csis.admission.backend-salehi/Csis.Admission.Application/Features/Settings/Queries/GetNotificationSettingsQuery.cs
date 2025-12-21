/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces.Settings;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Domain.Settings;

namespace Csis.Admission.Application.Features.Settings.Queries;

/// <summary>
/// دریافت تنظیمات نوتیفیکیشن
/// </summary>
public sealed record GetNotificationSettingsQuery : IRequest<SettingsModel<NotificationSettings>>;

internal sealed class GetNotificationSettingsQueryHandler(ISettingsService settingsService) : IRequestHandler<GetNotificationSettingsQuery, SettingsModel<NotificationSettings>>
{
    public async Task<SettingsModel<NotificationSettings>> Handle(GetNotificationSettingsQuery request, CancellationToken cancellationToken) {
        return await settingsService.GetAsync<NotificationSettings>();
    }
}
