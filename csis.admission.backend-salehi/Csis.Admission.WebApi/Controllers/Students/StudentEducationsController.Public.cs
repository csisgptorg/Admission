using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Educations.Dtos;
using Csis.Admission.Application.Features.Educations.Queries;
using Csis.Authorization.Services;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// تحصیل طلبه
/// </summary>
[Route("/api/public/educations"), Tags("Educations")]
public sealed class EducationsPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// EducationsPublicController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public EducationsPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<EducationDto>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetEducationByCodmQuery(codm)));
    }
}
