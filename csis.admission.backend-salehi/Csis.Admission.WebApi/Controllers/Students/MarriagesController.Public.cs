using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Utilities.Extensions;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Application.Features.Marriages.Commands;
using Csis.Admission.Application.Features.StudentDependents.Queries;
using Csis.Admission.Application.Features.StudentDependents.Dtos;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت موجودیت ازدواج
/// </summary>
[Route("/api/public/marriages"), Tags("Marriages")]
[CsisAuthorizeStudent]
public sealed class MarriagesPublicController(ICsisAuthenticatedUserService userService) : ApiControllerBase
{
    /// <summary>
    /// نمایش افراد تحت تکفل - همسران 
    /// </summary>
    /// <returns></returns>
    [HttpGet("dependent/spouses")]
    public async Task<IActionResult> GetMaleSpousesInfo() {
        var codm = (await userService.GetStudentCodmAsync()).ToInt();
        var result = await Mediator.Send(new GetDependentSpousesQuery(codm));
        return OkResult(result);
    }


    /// <summary>
    /// ثبت ازدواج طلاب خواهر 
    /// </summary>
    [HttpPost("student/sister")]
    public async Task<ActionResult> UpdateStudentSisterMarriage([FromBody] UpdateStudentSisterMarriageRequestCommand command) {
        command.Codm = (await userService.GetStudentCodmAsync()).ToInt();
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت ازدواج تکفل 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent")]
    public async Task<IActionResult> RegisterDependentMarriage([FromBody] UpdateChildMarriageRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
