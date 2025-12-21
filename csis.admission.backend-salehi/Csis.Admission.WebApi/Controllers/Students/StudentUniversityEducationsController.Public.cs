using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Utilities.Extensions;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.UniversityEducations.Dtos;
using Csis.Admission.Application.Features.UniversityEducations.Queries;
using Csis.Admission.Application.Features.UniversityEducations.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>تحصیلات دانشگاهی</summary>
[Route("api/public/student/university-educations"), Tags("StudentUniversityEducations"), CsisAuthorizeStudent]
public class StudentUniversityEducationsPublicController(ICsisAuthenticatedUserService authenticatedUser) : ApiControllerBase
{
    /// <summary>ثبت برای ایرانی</summary>
    [HttpPost("iranian")]
    public async Task<ActionResult<Result<StudentUniversityEducationDto>>> CreateStudentUniversityEducationIranian
        ([FromBody] CreateStudentUniversityEducationIranianRequestCommandAction command, [FromQuery] bool confirmed) {
        await Mediator.Send(new CreateStudentUniversityEducationIranianRequestCommand { Codm = null, TraceCode = command.TraceCode, Confirmed = confirmed });
        return NoContent();
    }

    /// <summary>ثبت برای غیر ایرانی</summary>
    [HttpPost("non-iranian")]
    public async Task<IActionResult> CreateStudentUniversityEducationRequest
        (CreateStudentUniversityEducationNonIranianRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ثبت برای تکفل ایرانی</summary>
    [HttpPost("dependent/iranian")]
    public async Task<ActionResult<Result<DependentUniversityEducationDto>>> CreateDependentUniversityEducationIranian
        ([FromBody] CreateDependentUniversityEducationIranianRequestCommandAction command, [FromQuery] bool confirmed) {
        await Mediator.Send(new CreateDependentUniversityEducationIranianRequestCommand {
            DependentId = command.DependentId,
            TraceCode = command.TraceCode,
            Confirmed = confirmed
        });
        return NoContent();
    }

    /// <summary>ثبت برای تکفل غیر ایرانی</summary>
    [HttpPost("dependent/non-iranian")]
    public async Task<IActionResult> CreateDependentUniversityEducationRequest
        (CreateDependentUniversityEducationNonIranianRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ایجاد تحصیلات دانشگاهی برای تکفل
    /// </summary>
    /// <param name="dependentId">شناسه تکفل</param>
    /// <param name="confirmed"></param>
    /// <returns></returns>
    [HttpPost("dependent/")]
    public async Task<IActionResult> CreateUniversityEducation(
        [FromQuery] int dependentId,
        [FromQuery] bool confirmed) {
        var codm = (await authenticatedUser.GetStudentCodmAsync()).ToInt();
        var result = await Mediator.Send(new CreateUniversityEducationCommand(codm, dependentId, confirmed));
        return OkResult(result);
    }

    /// <summary>تحصیلات دانشگاهی طلبه</summary>
    [HttpGet]
    public async Task<ActionResult<StudentUniversityEducationDto>> GetByCodm() {
        var codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentUniversityEducationsByCodmQuery(codm)));
    }

    /// <summary>تحصیلات دانشگاهی تکفل</summary>
    [HttpGet("dependent")]
    public async Task<ActionResult<DependentUniversityEducationDto>> GetDependentByCodm() {
        var codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetDependentUniversityEducationsByCodmQuery(codm)));
    }
}
