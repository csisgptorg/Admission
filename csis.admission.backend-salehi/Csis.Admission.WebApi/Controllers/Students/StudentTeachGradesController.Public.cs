using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.TeachGrades.Dtos;
using Csis.Admission.Application.Features.TeachGrades.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// رتبه تدریس
/// </summary>
[Route("/api/public/teach-grades"),Tags("TeachGrades")]
public sealed class TeachGradesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// TeachGradesPublicController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public TeachGradesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<TeachGradeDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetTeachGradesByCodmQuery(codm)));
    }
}
