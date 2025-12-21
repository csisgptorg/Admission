/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.Settings.Commands;
using Csis.Admission.Application.Features.Settings.Queries;
using Csis.Admission.Domain.Settings;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// مدیریت اپلیکیشن
/// </summary>
[Route("/api/private/app-management")]
public sealed class AppManagementController(IDistributedCacheService distributedCacheService) : ApiControllerBase
{
    /// <summary>
    /// ریست کش ردیس
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("reset-redis-cache"), CsisAuthorize(PermissionsEnum.ResetRedisCache)]
    public async Task<IActionResult> ResetRedisCache(CancellationToken cancellationToken) {
        await distributedCacheService.RemoveAllAsync(cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// دریافت تنظیمات نوتیفیکیشن
    /// </summary>
    /// <returns></returns>
    [HttpGet("notification"), CsisAuthorize(PermissionsEnum.ManageNotificationSettings)]
    public async Task<ActionResult<Result<SettingsModel<NotificationSettings>>>> GetNotificationSettings() {
        return OkResult(await Mediator.Send(new GetNotificationSettingsQuery()));
    }

    /// <summary>
    /// ذخیره تنظیمات نوتیفیکیشن
    /// </summary>
    /// <returns></returns>
    [HttpPost("notification"), CsisAuthorize(PermissionsEnum.ManageNotificationSettings)]
    public async Task<IActionResult> SaveNotificationSettings([FromBody] SaveNotificationSettingsCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
