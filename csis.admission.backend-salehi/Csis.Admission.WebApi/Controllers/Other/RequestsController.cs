using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Enums;
using Csis.Authorization;
using Csis.Paging;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>درخواست ها</summary>
[Route("/api/private/requests")]
public sealed class RequestsController : ApiControllerBase
{
    private readonly IRequestService _requestService;

    /// <inheritdoc/>
    public RequestsController(IRequestService requestService) {
        _requestService = requestService;
    }

    /// <inheritdoc/>
    [HttpGet("{id}"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<RequestDto>>> GetsById(long id) {
        return OkResult(await _requestService.GetById(id, CancellationToken.None));
    }

    /// <inheritdoc/>
    [HttpGet("by-codm/{codm}"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<List<RequestDto>>>> GetsByCodm([FromRoute] int codm) {
        return OkResult(await _requestService.GetAllByCodmAsync(codm, isCompleted: null, CancellationToken.None));
    }

    //TODO SkipSmsOnRejected گرفتن در آدرس خیلی جالب نیست - نیازمند بهبود
    /// <summary>تایید درخواست</summary>
    [HttpPut("{id}/approved"), CsisAuthorize(PermissionsEnum.StudentRequestApproved)]
    public async Task<IActionResult> Approved(long id, [FromQuery] bool SkipSmsOnRejected) {
        await _requestService.ApproveRequestByEmployee(new ApproveRequestByEmployeeCommand(id, ApprovalStatus.Approved, SkipSmsOnRejected), CancellationToken.None);
        return NoContent();
    }

    /// <summary>رد درخواست</summary>
    [HttpPut("{id}/rejected"), CsisAuthorize(PermissionsEnum.StudentRequestApproved)]
    public async Task<IActionResult> Rejected(long id, [FromQuery] bool SkipSmsOnRejected) {
        await _requestService.ApproveRequestByEmployee(new ApproveRequestByEmployeeCommand(id, ApprovalStatus.Rejected, SkipSmsOnRejected), CancellationToken.None);
        return NoContent();
    }

    /// <summary>درخواست های نیازمند تایید</summary>
    [HttpPost("to-approve"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<SearchPersonnelRequestsToApproveResult[]>>> SearchPersonnelRequestsToApprove(GetEmployeeRequestsToApproveQuery query) {
        return PaginatedResult(await _requestService.GetEmployeeRequestsToApprove(query, CancellationToken.None));
    }

    /// <summary>
    /// تمام درخواست ها
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("all"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<IPagedList<RequestDto>>>> GetAll([FromBody] AllRequestQuery query) {
        return PaginatedResult(await _requestService.GetAllAsync(query, CancellationToken.None));
    }

    /// <summary>
    /// مقایسه کامل درخواست با داده‌های فعلی
    /// <para>شامل: داده فعلی دیتابیس + تغییرات درخواستی + لیست تفاوت‌ها</para>
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="cancellationToken">توکن لغو</param>
    [HttpGet("{requestId}/comparison"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<IActionResult> GetRequestComparisonDetail([FromRoute] long requestId, CancellationToken cancellationToken) {
        var result = await _requestService.GetRequestComparisonDetailAsync(requestId, cancellationToken);
        return OkResult(result);
    }
}
