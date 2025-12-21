using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.UniversityEducations.Commands;
using Csis.Admission.Application.Features.UniversityEducations.Dtos;
using Csis.Admission.Application.Features.UniversityEducations.Queries;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>تحصیلات دانشگاهی</summary>
[Route("/api/private/university-educations")]
public sealed class StudentUniversityEducationsController : ApiControllerBase
{
    /// <summary>ثبت برای ایرانی</summary>
    [HttpPost("iranian")]
    public async Task<ActionResult<Result<StudentUniversityEducationDto>>> CreateUniversityEducationIranian
        ([FromBody] CreateStudentUniversityEducationIranianRequestCommandAction command, [FromQuery] bool confirmed) {
        await Mediator.Send(new CreateStudentUniversityEducationIranianRequestCommand { Codm = command.Codm, TraceCode = command.TraceCode, Confirmed = confirmed });
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

    /// <summary>تحصیلات دانشگاهی طلبه</summary>
    [HttpGet("student"), CsisAuthorize(PermissionsEnum.StudentUniversityEducationRegister)]
    public async Task<ActionResult<StudentUniversityEducationDto>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentUniversityEducationsByCodmQuery(codm)));
    }

    /// <summary>تحصیلات دانشگاهی تکفل</summary>
    [HttpGet("dependent"), CsisAuthorize(PermissionsEnum.StudentUniversityEducationRegister)]
    public async Task<ActionResult<DependentUniversityEducationDto>> GetDependentByCodm([FromQuery] int codm, [FromQuery] long? dependentId) {
        return OkResult(await Mediator.Send(new GetDependentUniversityEducationsByCodmQuery(codm, dependentId)));
    }

    /// <summary>
    /// ارتباطات داده ای
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<ActionResult<Result<int>>> DataImport([FromBody] UniversityEducationDataImportCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>
    /// حذف تحصیلات دانشگاهی
    /// </summary>
    /// <param name="Codm"></param>
    /// <param name="EducationId"></param>
    /// <returns></returns>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentUniversityEducationDelete)]
    public async Task<ActionResult> DeleteUniversityEducation( [FromQuery] int EducationId) {
        await Mediator.Send(new DeleteStudentUniversityEducationRequestCommand( EducationId));
        return NoContent();
    }

    /// <summary>
    /// حذف تحصیلات دانشگاهی تکفل
    /// </summary>
    /// <param name="Codm"></param>
    /// <param name="DependentId"></param>
    /// <param name="EducationId"></param>
    /// <returns></returns>
    [HttpDelete("dependent"), CsisAuthorize(PermissionsEnum.DependentUniversityEducationDelete)]
    public async Task<ActionResult> DeleteDependentUniversityEducation( [FromQuery] int EducationId) {
        await Mediator.Send(new DeleteDependentUniversityEducationRequestCommand( EducationId));
        return NoContent();
    }
}
