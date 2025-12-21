using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.PreachGrades.Dtos;
using Csis.Admission.Application.Features.PreachGrades.Queries;
using Csis.Authorization.Services;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// رتبه تبلیغی
/// </summary>
[Route("/api/public/preach-grades"),Tags("PreachGrades")]
public sealed class PreachGradesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// PreachGradesPublicController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public PreachGradesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<PreachGradeDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetPreachGradesByCodmQuery(codm)));
    }
}
