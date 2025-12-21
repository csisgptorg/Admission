using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.ImamJamaat.Commands;
using Csis.Admission.Application.Features.ImamJamaat.Dtos;
using Csis.Admission.Application.Features.ImamJamaat.Queries;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Csis.Paging;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.ImamJamaat;

/// <summary>
/// ادمین امام جماعت
/// </summary>
[Route("api/private/imamjamaat-admin")]
public class ImamJamaatAdminController : ApiControllerBase
{
    /// <summary>
    /// گرفتن لیست مساجد با قابلیت جستجو و فیلتر
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("mosque-list"), CsisAuthorize(PermissionsEnum.GetMosqueListView)]
    [DynamicSearch<Domain.Entities.ImamJamaat>]
    public async Task<ActionResult<Result<IPagedList<MosqueListDto>>>> Search([FromBody] GetMosqueListQuery query) {
        return PaginatedResult(await Mediator.Send(query));
    }

    /// <summary>
    /// گرفتن لیست تمامی درخواست ها
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("all-request-list"), CsisAuthorize(PermissionsEnum.GetAllRequestList)]
    [DynamicSearch<Domain.Entities.ImamJamaat>]
    public async Task<ActionResult<Result<IPagedList<MosqueListDto>>>> SearchAllRequest([FromBody] GetMosqueListQuery query) {
        return PaginatedResult(await Mediator.Send(query));
    }


    /// <summary>
    /// گرفتن اطلاعات طلبه بر اساس کد مرکز
    /// </summary>
    [HttpGet("{codm:min(1)}"), CsisAuthorize(PermissionsEnum.ImamJamaatCodMInquiry)]
    public async Task<ActionResult<Result<StudentDto>>> GetByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentByCodmQuery(codm)));
    }

    /// <summary>
    /// گرفتن اطلاعات مسجد بر اساس شناسه
    /// </summary>
    /// <param name="mosqueId"></param>
    /// <returns></returns>
    [HttpGet("mosque/{mosqueId:min(1)}"), CsisAuthorize(PermissionsEnum.GetMosqueById)]
    public async Task<ActionResult<Result<MosqueByIdDto>>> GetMosqueById([FromRoute] int mosqueId) {
        return OkResult(await Mediator.Send(new GetMosqueByIdQuery(mosqueId)));
    }

    /// <summary>
    /// ایجاد مسجد با جزئیات
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("mosque"), CsisAuthorize(PermissionsEnum.CreateMosqueWithDetails)]
    public async Task<ActionResult<Result<int>>> CreateStudent([FromBody] CreateMosqueWithDetailsCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetMosqueById), new { mosqueId = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// به‌روزرسانی مسجد با جزئیات
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("mosque"), CsisAuthorize(PermissionsEnum.UpdateMosqueWithDetails)]
    public async Task<ActionResult<Result<Task>>> UpdateStudent([FromBody] UpdateMosqueWithDetailsCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف مسجد بر اساس شناسه
    /// </summary>
    /// <param name="mosqueId"></param>
    /// <returns></returns>
    [HttpDelete("mosque/{mosqueId:min(1)}"), CsisAuthorize(PermissionsEnum.DeleteMosqueWithDetails)]
    public async Task<ActionResult<Result>> DeleteMosque([FromRoute] int mosqueId) {
        await Mediator.Send(new DeleteMosqueCommand(mosqueId));
        return NoContent();
    }

    /// <summary>
    /// گرفتن آدرس بر اساس کد پستی
    /// </summary>
    /// <param name="postalcode"></param>
    /// <returns></returns>
    [HttpGet("mosque/postal-code/{postalcode:long}"), CsisAuthorize(PermissionsEnum.GetAddressByPostalCode)]
    public async Task<ActionResult<Result<MosqueAddressFromExternalServiceDto>>> GetAddressByPostalCode([FromRoute] long postalcode) {
        return OkResult(await Mediator.Send(new GetAddressByPostalCodeQuery(postalcode)));
    }

    /// <summary>
    /// بررسی امکان ثبت مسجد برای طلبه
    /// </summary>
    /// <param name="codM"></param>
    /// <returns></returns>
    [HttpGet("can-register")]
    public async Task<ActionResult<Result<bool>>> CanRegister([FromQuery] int codM) {

        return OkResult(await Mediator.Send(new ImamJamaatCanRegisterQuery(codM)));
    }

    /// <summary>
    /// گرفتن لیست مساجد بر اساس آدرس
    /// </summary>
    /// <returns></returns>
    [HttpPost("mosque-by-address")]
    public async Task<ActionResult<Result<List<MosqueAddressDto>>>> GetMosqueByAddress([FromBody] GetMosqueByAddressCommand command) {
        return OkResult(await Mediator.Send(command));
    }
}
