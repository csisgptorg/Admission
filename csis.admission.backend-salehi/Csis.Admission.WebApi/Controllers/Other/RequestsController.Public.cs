using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Domain.Enums;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>درخواست ها</summary>
[Route("/api/public/student/requests"), Tags("Requests"), CsisAuthorizeStudent]
public sealed class RequestsPublicController : ApiControllerBase
{
    private readonly IRequestService _requestService;

    /// <inheritdoc/>
    public RequestsPublicController(IRequestService requestService) {
        _requestService = requestService;
    }

    /// <summary>لیست درخواست های طلبه</summary>
    [HttpGet]
    public async Task<ActionResult<Result<List<RequestDto>>>> GetsByCodm() {
        return OkResult(await _requestService.GetAllByCodmAsync(codm:null,isCompleted:null,CancellationToken.None));
    }

    /// <summary>نمایش درخواست</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<RequestDto>>> GetsById(long id) {
        return OkResult(await _requestService.GetById(id, CancellationToken.None));
    }

    /// <summary>درخواست های نیازمند تایید</summary>
    [HttpGet("to-approve")]
    public async Task<ActionResult<Result<RequestsToApproveDto[]>>> GetRequestsToApprove() {
        return OkResult(await _requestService.GetStudentRequestsToApprove(CancellationToken.None));
    }

    /// <summary>تایید درخواست</summary>
    [HttpPut("{id}/approved")]
    public async Task<IActionResult> Approved(long id) {
        await _requestService.ApproveRequestByStudent(new ApproveRequestByStudentCommand(id, ApprovalStatus.Approved), CancellationToken.None);
        return NoContent();
    }

    /// <summary>رد درخواست</summary>
    [HttpPut("{id}/rejected")]
    public async Task<IActionResult> Rejected(long id) {
        await _requestService.ApproveRequestByStudent(new ApproveRequestByStudentCommand(id,ApprovalStatus.Rejected), CancellationToken.None);
        return NoContent();
    }
}
