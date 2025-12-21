using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.WebApi.Filters;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Veterans.Dtos;
using Csis.Admission.Application.Features.Veterans.Queries;
using Csis.Admission.Application.Features.Veterans.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>ایثارگری</summary>
[Route("/api/private/veterans")]
public sealed class StudentVeteransController : ApiControllerBase
{
    /// <summary>دریافت اطلاعات ایثارگری طلبه</summary>
    /// <param name="codm">کد مرکز خدمات</param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentVeteranView)]
    public async Task<ActionResult<Result<VeteranDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetVeteranByCodmQuery(codm)));
    }

    /// <summary>ثبت یا بروزرسانی اطلاعات ایثارگری</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentVeteranCreateOrUpdate)]
    public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateVeteranRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف اطلاعات ایثارگری</summary>
    [HttpDelete, CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.StudentVeteranDelete)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteVeteranRequestCommand(codm, id));
        return NoContent();
    }

    /// <summary>ویرایش نسبت با شهید</summary>
    [HttpPost("relation-with-martyr"), CsisAuthorizeApiKey(PermissionsEnum.StudentVeteranUpdateRelationWithMartyr),ApiKeyHeader]
    public async Task<IActionResult> UpdateRelationWithMartyr(CreateORUpdateVeteranRelationWithMartyrCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ویرایش نسبت با شهید</summary>
    [HttpPost("veteran-percent"), CsisAuthorizeApiKey(PermissionsEnum.StudentVeteranUpdateVeteranPercent), ApiKeyHeader]
    public async Task<IActionResult> UpdateVeteranPercent(CreateORUpdateVeteranPercentCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ویرایش روز آزدگی</summary>
    [HttpPost("captivity-days"), CsisAuthorizeApiKey(PermissionsEnum.StudentVeteranUpdateCaptivityDays), ApiKeyHeader]
    public async Task<IActionResult> UpdateCaptivityDays(CreateORUpdateVeteranCaptivityDaysCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
