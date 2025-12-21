using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Pregnancies.Dtos;
using Csis.Admission.Application.Features.Pregnancies.Queries;
using Csis.Admission.Application.Features.Pregnancies.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// ایام بارداری
/// </summary>
[Route("/api/private/pregnancies")]
public sealed class StudentPregnanciesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentPregnancyView)]
    public async Task<ActionResult<Result<List<PregnancyDto>>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetPregnancyByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentPregnancyRegister)]
    public async Task<IActionResult> RegisterRequest([FromBody] CreatePregnancyRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
