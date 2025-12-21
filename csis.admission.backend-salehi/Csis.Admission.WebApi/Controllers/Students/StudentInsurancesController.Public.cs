using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.Insurances.Dtos;
using Csis.Admission.Application.Features.Insurances.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>بیمه</summary>
[Route("/api/public/student/insurances"), Tags("StudentInsurances"), CsisAuthorizeStudent]
public sealed class StudentInsurancesPublicController : ApiControllerBase
{
    private readonly ICsisWsmService _wsmService;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentInsurancesPublicController(ICsisWsmService wsmService,ICsisAuthenticatedUserService authenticatedUserService) {
        _wsmService = wsmService;
        _authenticatedUserService = authenticatedUserService;
    }

    /// <inheritdoc/>
    [HttpGet]
    public async Task<ActionResult<StudentDependentInsuranceDto>> Get([FromQuery]long? dependentId) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentDependentInsurancesByCodmQuery(codm, dependentId)));
    }
}
