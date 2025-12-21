using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Veterans.Dtos;
using Csis.Admission.Application.Features.Veterans.Queries;
using Csis.Admission.Application.Features.Veterans.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// ایثارگری
/// </summary>
[Route("/api/public/veterans"), Tags("StudentVeterans")]
public sealed class VeteransPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// VeteransController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public VeteransPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// دریافت اطلاعات ایثارگری
    /// </summary>
    /// <returns></returns>
    [HttpGet,CsisAuthorizeStudent]
    public async Task<ActionResult<Result<VeteranDto>>> Get() {
        var codm =int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetVeteranByCodmQuery(codm)));
    }

    /// <summary>
    /// ثبت یا بروزرسانی اطلاعات ایثارگری
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    //[HttpPost, CsisAuthorizeStudent]
    //public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateVeteranRequestCommand command) {
    //    await Mediator.Send(command);
    //    return NoContent();
    //}
}
