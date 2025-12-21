using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.AdmissionAuditLogs.Dtos;
using Csis.Admission.Application.Features.AdmissionAuditLogs.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// سوابق اطلاعات پذیرش
/// </summary>
[Route("/api/private/audit-logs")]
public sealed class AdmissionAuditLogsController : ApiControllerBase
{
    /// <summary>سوابق طلبه</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<StudentAdmissionAuditLogDto[]>>> GetStudentAuditLogs([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentAdmissionAuditLogsByCodmQuery(codm)));
    }

    /// <summary>سوابق تکفل</summary>
    [HttpGet("dependents"), CsisAuthorize]
    public async Task<ActionResult<Result<DependentAdmissionAuditLogDto[]>>> GetDependentAuditLogs([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetDependentAdmissionAuditLogsByCodmQuery(codm)));
    }
}
