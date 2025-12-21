using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using Csis.Admission.Application.Features.DependentCaseActive.Queries;
using Csis.Admission.Application.Features.Marriages.Dtos;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Application.Features.StudentDependents.Commands;
using Csis.Admission.Application.Features.StudentDependents.Dtos;
using Csis.Admission.Application.Features.StudentDependents.Queries;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers;

/// <inheritdoc/>
[Route("/api/private/student-dependents"), CsisAuthorize]
public sealed class StudentDependentsController : ApiControllerBase
{
    /// <summary>
    /// اطلاعات همسر طلبه بر اساس کد مرکز
    /// </summary>
    /// <param name="codM"></param>
    /// <returns></returns>
    [HttpGet("spouse/{codM}"), CsisAuthorize(PermissionsEnum.DependentSpouseInfoView)]
    public async Task<ActionResult<Result<StudentSpouseDto[]>>> GetSpouse([FromRoute] int codM) {
        return OkResult(await Mediator.Send(new GetStudentSpouseByStudentCodmQuery(codM)));
    }

    /// <summary>
    /// تغییرات پرونده برای یکی از افراد تحت تکفل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("case-description"), CsisAuthorize(PermissionsEnum.DependentCaseDescriptionCreate)]
    public async Task<ActionResult<Result<long>>> CreateCaseDescription([FromBody] CreateStudentDependentCaseDescriptionRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// غیرفعال کردن پرونده برای یکی از افراد تحت تکفل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("case-active-status"), CsisAuthorize(PermissionsEnum.DependentCaseActiveUpdate, PermissionsEnum.SeniorPersonnel)]
    public async Task<ActionResult<Result<long>>> UpdateCaseActive([FromBody] UpdateDependentCaseActiveStatusSeniorRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }   

    /// <summary>
    /// تعیین وضعیت پرونده برای یکی از افراد تحت تکفل توسط کارمند به صورت خودکار
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("auto-case-active"), CsisAuthorize(PermissionsEnum.DependentCaseActiveEmployeeUpdate)]
    public async Task<ActionResult<Result<long>>> UpdateCaseActiveEmployee([FromBody] UpdateDependentCaseActiveEmployeeRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// محاسبه علت فعال یا غیر فعال بودن تکفل
    /// </summary>
    /// <param name="codM"></param>
    /// <param name="dependentId"></param>
    /// <returns></returns>
    [HttpGet("calculate-reason"), CsisAuthorize(PermissionsEnum.DependentCaseActiveEmployeeUpdate)]
    public async Task<ActionResult<Result<ActiveDeActiveReasonDiffrenceDto>>> CalculateDependentReason([FromQuery] int codM, [FromQuery] long dependentId) {
        var result = await Mediator.Send(new GetActiveDeActiveReasonDiffrenceQuery(codM, dependentId));
        return OkResult(result);
    }

    /// <summary>
    /// ثبت اطلاعات همسر طلبه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("spouse-registry")]
    public async Task<IActionResult> SpouseRegistry([FromBody] StudentSpouseRegistryRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// شناسایی همسر از ثبت احوال
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("identify-spouse")]
    public async Task<ActionResult<Result<SpouseIdentifyDto>>> IdentifySpouse([FromBody] IdentifySpouseFromSabteAhvalCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// ثبت اطلاعات فرزند طلبه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("child-registry")]
    public async Task<IActionResult> ChildRegistry([FromBody] StudentChildRegistryCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// اطلاعات همسر بر اساس کد مرکز
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("spouse")]
    public async Task<ActionResult<Result<List<DependentSpousesDto>>>> GetSpouseInfo(int codm) {
        var spouse = await Mediator.Send(new GetDependentSpousesQuery(codm));
        return OkResult(spouse);
    }

    /// <summary>
    /// اطلاعات افراد تحت تکفل بر اساس کد مرکز
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("dependent")]
    public async Task<ActionResult<Result<List<FamilyInfoDto>>>> GetDependentInfo(int codm) {
        var dependents = await Mediator.Send(new GetFamilySinglesByCodmQuery { Codm = codm });
        return OkResult(dependents);
    }
}
