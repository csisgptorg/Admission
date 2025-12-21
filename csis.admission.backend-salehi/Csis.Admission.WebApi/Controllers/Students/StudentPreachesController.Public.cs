using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Preaches.Dtos;
using Csis.Admission.Application.Features.Preaches.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// تبلیغ
/// </summary>
[Route("/api/public/preaches"), Tags("Preaches")]
public sealed class PreachesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// PreachesPublicController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public PreachesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<PreachDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetPreachesByCodmQuery(codm)));
    }
}
