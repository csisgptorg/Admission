using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.CommissionInfos.Dtos;
using Csis.Admission.Application.Features.CommissionsInfos.Dtos;
using Csis.Admission.Application.Features.CommissionInfos.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>کمسیون</summary>
[Route("/api/private/commission-info")]
public sealed class StudentCommissionsInfoController : ApiControllerBase
{
    /// <summary>کمسیون طلبه</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentCommissionInfoView)]
    public async Task<ActionResult<Result<StudentCommissionInfoDto>>> GetStudentCommissionInfo([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentCommissionsInfoByCodmQuery(codm)));
    }

    /// <summary>کمسیون طلبه</summary>
    [HttpGet("dependents"), CsisAuthorize(PermissionsEnum.StudentCommissionInfoView)]
    public async Task<ActionResult<Result<DependentCommissionInfoDto>>> GetDependentCommissionInfo([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetDependentCommissionsInfoByCodmQuery(codm)));
    }
}
