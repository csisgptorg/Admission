using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Pregnancies.Dtos;
using Csis.Admission.Application.Features.Pregnancies.Queries;
using Csis.Admission.Application.Features.Pregnancies.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>ایام بارداری</summary>
[Route("/api/public/pregnancies"), Tags("StudentPregnancies"), CsisAuthorizeStudent]
public sealed class StudentPregnanciesPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// PregnanciesController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public StudentPregnanciesPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <inheritdoc/>
    [HttpGet]
    public async Task<ActionResult<Result<List<PregnancyDto>>>> GetByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetPregnancyByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpPost]
    public async Task<IActionResult> RegisterRequest([FromBody] CreatePregnancyRequestCommand command) {
        command.Codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete Pregnancy by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await Mediator.Send(new DeletePregnancyCommand(id));
        return NoContent();
    }

}
