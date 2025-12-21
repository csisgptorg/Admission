using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.BlockServices.Dtos;
using Csis.Admission.Application.Features.BlockServices.Queries;
using Csis.Admission.Application.Features.BlockServices.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>خدمات مسدود</summary>
[Route("/api/private/block-services")]
public sealed class StudentBlockServicesController : ApiControllerBase
{
    /// <summary>ثبت</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.CreateStudentBlockService)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> Create([FromBody] CreateStudentBlockServiceCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>ویرایش</summary>
    [HttpPut, CsisAuthorize(PermissionsEnum.UpdateStudentBlockService)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> Update([FromBody] UpdateStudentBlockServiceCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف</summary>
    [HttpDelete("{id}"), CsisAuthorize(PermissionsEnum.DeleteStudentBlockService)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> Delete(int id) {
        await Mediator.Send(new DeleteStudentBlockServiceCommand(id));
        return NoContent();
    }

    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentBlockServiceViews)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> GetAllByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new StudentBlockedServicesByCodmQuery(codm)));
    }

    /// <summary>لیست خدمات</summary>
    [HttpGet("services"), CsisAuthorize(PermissionsEnum.StudentServiceViews)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> GetServicesByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetCsisServicesByCodmQuery(codm)));
    }

    /// <summary>ثبت</summary>
    [HttpPost("dependent"), CsisAuthorize(PermissionsEnum.CreateStudentBlockService)]
    public async Task<ActionResult<Result<List<DependentBlockServiceDto>>>> CreateDependent([FromBody] CreateDependentBlockServiceCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>ویرایش</summary>
    [HttpPut("dependent"), CsisAuthorize(PermissionsEnum.UpdateStudentBlockService)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> UpdateDependent([FromBody] UpdateDependentBlockServiceCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف</summary>
    [HttpDelete("dependent/{id}"), CsisAuthorize(PermissionsEnum.DeleteStudentBlockService)]
    public async Task<ActionResult<Result<List<StudentBlockServiceDto>>>> DeleteDependent(int id) {
        await Mediator.Send(new DeleteDependentBlockServiceCommand(id));
        return NoContent();
    }

    /// <summary>لیست</summary>
    [HttpGet("dependent"), CsisAuthorize(PermissionsEnum.DependentBlockServiceViews)]
    public async Task<ActionResult<Result<List<DependentBlockServiceDto>>>> GetAllDependentByCodm([FromQuery] int codm, long dependentId) {
        return OkResult(await Mediator.Send(new DependentBlockedServicesQuery(codm,dependentId)));
    }

    /// <summary>لیست خدمات</summary>
    [HttpGet("dependent/services"), CsisAuthorize(PermissionsEnum.DependentServiceViews)]
    public async Task<ActionResult<Result<List<DependentBlockServiceDto>>>> GetServicesByDependent([FromQuery] long dependentId) {
        return OkResult(await Mediator.Send(new GetCsisServicesByDependentQuery(dependentId)));
    }
}
