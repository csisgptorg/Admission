using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Enums;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>درخواست ها</summary>
[Route("/api/private/caseFillingRequests")]
public sealed class CaseFillingRequestsController(ICaseFillingRequestService requestService) : ApiControllerBase
{
    
    /// <inheritdoc/>
    [HttpGet("{id}"),CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<RequestDto>>> GetsById(long id) {
        return OkResult(await requestService.GetById(id, CancellationToken.None));
    }

    //TODO SkipSmsOnRejected گرفتن در آدرس خیلی جالب نیست - نیازمند بهبود
    /// <summary>تایید درخواست</summary>
    [HttpPut("{id}/approved"), CsisAuthorize(PermissionsEnum.StudentRequestApproved)]
    public async Task<IActionResult> Approved(long id, [FromQuery] bool SkipSmsOnRejected) {
        await requestService.ApproveRequestByEmployee(new ApproveCaseFillingRequestByEmployeeCommand(id,ApprovalStatus.Approved,SkipSmsOnRejected), CancellationToken.None);
        return NoContent();
    }

    /// <summary>رد درخواست</summary>
    [HttpPut("{id}/rejected"), CsisAuthorize(PermissionsEnum.StudentRequestApproved)]
    public async Task<IActionResult> Rejected(long id, [FromQuery] bool SkipSmsOnRejected) {
        await requestService.ApproveRequestByEmployee(new ApproveCaseFillingRequestByEmployeeCommand(id, ApprovalStatus.Rejected, SkipSmsOnRejected), CancellationToken.None);
        return NoContent();
    }

    /// <summary>درخواست های نیازمند تایید</summary>
    [HttpPost("to-approve"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<SearchPersonnelCaseFillingRequestsToApproveResult[]>>> SearchPersonnelRequestsToApprove(GetEmployeeCaseFillingRequestsToApproveQuery query) {
        return OkResult(await requestService.GetEmployeeRequestsToApprove(query, CancellationToken.None));
    }

    /// <summary> درخواست های نیازمند تایید </summary> 
    [HttpPost("all-request"), CsisAuthorize(PermissionsEnum.StudentRequestView)]
    public async Task<ActionResult<Result<SearchPersonnelCaseFillingRequestsToApproveResult[]>>> GetAllCaseFillingRequest(GetEmployeeCaseFillingRequestsToApproveQuery query) {
        return OkResult(await requestService.GetAllCaseFillingRequest(query, CancellationToken.None));
    }
}
