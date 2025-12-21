using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Teaches.Dtos;
using Csis.Admission.Application.Features.Teaches.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// تدریس
/// </summary>
[Route("api/public/teaches"), Tags("Teaches")]
public sealed class TeachesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public TeachesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<TeachDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetTeachesByCodmQuery(codm)));
    }
}
