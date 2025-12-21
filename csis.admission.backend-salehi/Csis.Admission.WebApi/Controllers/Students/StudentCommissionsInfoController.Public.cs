using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.CommissionInfos.Dtos;
using Csis.Admission.Application.Features.CommissionsInfos.Dtos;
using Csis.Admission.Application.Features.CommissionInfos.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>کمسیون</summary>
[Route("/api/public/commission-info"),Tags("StudentCommissionsInfo"),CsisAuthorizeStudent]
public sealed class StudentCommissionsInfoPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentCommissionsInfoPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>کمسیون طلبه</summary>
    [HttpGet]
    public async Task<ActionResult<Result<StudentCommissionInfoDto>>> GetStudentCommissionInfo() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentCommissionsInfoByCodmQuery(codm)));
    }

    /// <summary>کمسیون طلبه</summary>
    [HttpGet("dependents")]
    public async Task<ActionResult<Result<DependentCommissionInfoDto>>> GetDependentCommissionInfo() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetDependentCommissionsInfoByCodmQuery(codm)));
    }
}
