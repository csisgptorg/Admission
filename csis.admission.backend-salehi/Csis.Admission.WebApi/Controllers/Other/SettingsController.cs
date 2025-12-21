using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Settings.People.Commands;
using Csis.Admission.Application.Features.Settings.People.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>تنظیمات</summary>
[Route("/api/private/settings")]
public sealed class SettingsController : ApiControllerBase
{
    /// <summary>
    /// دریافت تنظیمات پنل افراد
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("people")]
    public async Task<ActionResult> GetSettings() {
        var result = await Mediator.Send(new GetSettingsQuery());
        return OkResult(result);
    }

    /// <summary>
    /// تنظیمات پنل افراد
    /// </summary>
    /// <returns></returns>
    [HttpPost("people"), CsisAuthorize(PermissionsEnum.SetPeoplePanelSettings)]
    public async Task<ActionResult> GetPeoplePanelSettings([FromBody] CreateOrUpdateSettingsCommand command) {
        await Mediator.Send(command);
        return NoContent(); 
    }
}
