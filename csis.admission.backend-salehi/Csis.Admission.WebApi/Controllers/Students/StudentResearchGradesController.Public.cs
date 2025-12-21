using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.ResearchGrades.Dtos;
using Csis.Admission.Application.Features.ResearchGrades.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// رتبه پژوهشی
/// </summary>
[Route("/api/public/research-grades"), Tags("ResearchGrades")]
public sealed class ResearchGradesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// ResearchGradesPublicController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public ResearchGradesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<ResearchGradeDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetResearchGradesByCodmQuery(codm)));
    }
}
