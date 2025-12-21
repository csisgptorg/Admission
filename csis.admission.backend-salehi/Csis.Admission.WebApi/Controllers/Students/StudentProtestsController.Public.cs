using Csis.Admission.Application.Features.Protests.Commands;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Protests.Dtos;
using Csis.Admission.Application.Features.Protests.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// اعتراضات
/// </summary>
[Route("/api/public/protests"), Tags("StudentProtests")]
public sealed class ProtestsPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public ProtestsPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// دریافت اعتراضات بر اساس کد مرکز
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<List<ProtestDto>>> GetByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetProtestsByCodmQuery(codm)));
    }

    /// <summary>
    /// ثبت اعتراض بر اساس کد مرکز
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateProtestByCodmRequestCommand command) {

        await Mediator.Send(command);
        return NoContent();
    }
}
