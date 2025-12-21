using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Divorce.Commands;
using Csis.Admission.Application.Features.Divorce.Queries;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Application.Features.StudentDependents.Dtos;
using Csis.Authorization;
using Csis.Authorization.Services;
using Csis.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت موجودیت ازدواج
/// </summary>
[Route("/api/public/divorces"), Tags("Divorces")]
[CsisAuthorizeStudent]
public sealed class DivorcesPublicController(ICsisAuthenticatedUserService userService) : ApiControllerBase
{
    /// <summary>
    /// نمایش مشخصات همسران برای سرپرست مرد 
    /// </summary>
    /// <returns></returns>
    [HttpGet("dependent/spouse")]
    public async Task<IActionResult> GetDependentSpouses() {
        var codm = (await userService.GetStudentCodmAsync()).ToInt();
        var result = await Mediator.Send(new GetDependentSpousesDivorceQuery(codm));
        return OkResult(result);
    }

    /// <summary>
    /// ثبت طلاق طلاب خواهر
    /// </summary>
    /// <returns></returns>
    [HttpPost("student/sister")]
    public async Task<IActionResult> UpdateStudentSisterDivorce([FromBody] UpdateStudentSisterDivorceRequestCommand command) {
        var codm = (await userService.GetStudentCodmAsync()).ToInt();
        command.Codm = codm;

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق  تحت تکفل 
    /// </summary>
    /// <returns></returns>
    [HttpPost("dependent")]
    public async Task<IActionResult> UpdateDependentWifesDivorce([FromBody] UpdateDependentDivorceRequestCommand command) {
        command.Codm = (await userService.GetStudentCodmAsync()).ToInt();

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق همسر
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("wife")]
    public async Task<IActionResult> UpdateWifeDivorce([FromBody] UpdateWifeDivorceRequestCommand command) {
        command.Codm = (await userService.GetStudentCodmAsync()).ToInt();
        await Mediator.Send(command);
        return NoContent();
    }
}
