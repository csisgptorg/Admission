using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Researches.Dtos;
using Csis.Admission.Application.Features.Researches.Queries;

namespace Csis.Admission.WebApi.Controllers;
/// <summary>
/// پژوهش
/// </summary>
[Route("/api/public/researches"), Tags("Researches")]
public sealed class ResearchesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// ResearchesPublicController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public ResearchesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<ResearchDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetResearchesByCodmQuery(codm)));
    }
}
