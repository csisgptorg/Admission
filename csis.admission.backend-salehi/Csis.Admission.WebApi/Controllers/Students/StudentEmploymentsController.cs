using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.WebApi.Filters;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Employments.Dtos;
using Csis.Admission.Application.Features.Employments.Queries;
using Csis.Admission.Application.Features.Employments.Commands;
using Csis.Admission.Application.Features.DependentEmployments.Queries;
using Csis.Admission.Application.Features.DependentEmployments.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>شغل و درآمد</summary>
[Route("/api/private/employments")]
public sealed class StudentEmploymentsController : ApiControllerBase
{
    /// <summary>ثبت یا بروز رسانی</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentEmploymentRegister)]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateStudentEmploymentRequestCommand command) {
        command.Confirmed = true;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ثبت وضعیت اشتغال تکفل</summary>
    [HttpPost("dependent"), CsisAuthorize(PermissionsEnum.DependentEmploymentRegister)]
    public async Task<IActionResult> CreateDependent([FromBody] CreateOrUpdateDependentEmploymentRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>دریافت وضعیت اشتغال طلبه</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentEmploymentView)]
    public async Task<ActionResult<Result<StudentEmploymentDto>>> GetAllByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentEmploymentByCodmQuery(codm)));
    }

    /// <summary>دریافت وضعیت اشتغال تکفل</summary>
    [HttpGet("dependent")]
    public async Task<ActionResult<Result<List<StudentEmploymentDto>>>> GetDependentsEmploymentByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetDependentsEmploymentByCodmQuery(codm)));
    }

    /// <summary>ارتباط داده ای</summary>
    [HttpPost("student/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<ActionResult<Result<long>>> DataImport([FromBody] EmployeeDataImportCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }


    /// <inheritdoc/>
    [HttpPost("dependent/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<IActionResult> DataImport([FromBody] EmploymentDependentDataImportCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف اشتغال طلبه</summary>
    [HttpDelete, CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.StudentEmploymentDelete)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteStudentEmploymentRequestCommand(codm, id));
        return NoContent();
    }

    /// <summary>حذف اشتغال تکفل</summary>
    [HttpDelete("dependent"), CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.DependentEmploymentDelete)]
    public async Task<IActionResult> DeleteDependent([FromQuery] int codm, [FromQuery] int id, [FromQuery] long dependentId) {
        await Mediator.Send(new DeleteDependentEmploymentRequestCommand(codm, id, dependentId));
        return NoContent();
    }

    /// <summary>شناسایی موردی اشتغال</summary>
    [HttpPost("identify"), CsisAuthorize(PermissionsEnum.StudentEmploymentIdentify)]
    public async Task<ActionResult<Result<long>>> IdentifyEmployment([FromBody] IdentifyStudentEmploymentRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }
    /// <summary>شناسایی موردی اشتغال</summary>
    [HttpGet("identify"), CsisAuthorize(PermissionsEnum.StudentEmploymentIdentify)]
    public async Task<ActionResult<Result<List<EmployeeIdentificationDto>>>> GetIdentifyEmployment([FromQuery] int codm) {
        var result = await Mediator.Send(new GetIdentifyStudentEmploymentQuery(codm));
        return OkResult(result);
    }
}
