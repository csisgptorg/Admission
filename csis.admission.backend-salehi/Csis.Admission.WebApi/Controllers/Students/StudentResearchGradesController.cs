using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.ResearchGrades.Dtos;
using Csis.Admission.Application.Features.ResearchGrades.Queries;
using Csis.Admission.Application.Features.ResearchGrades.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>رتبه پژوهشی</summary>
[Route("/api/private/research-grades")]
public sealed class StudentResearchGradesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentResearchGradeView)]
    public async Task<ActionResult<Result<ResearchGradeDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetResearchGradeByIdQuery(id)));
    }

    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentResearchGradeView)]
    public async Task<ActionResult<Result<ResearchGradeDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetResearchGradesByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentResearchGradeRegister)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateResearchGradeCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id = result}, Result<int>.Success(result));
    }

    /// <inheritdoc/>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentResearchGradeRegister)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateResearchGradeCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <inheritdoc/>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentResearchGradeRegister, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteResearchGradeRequestCommand(codm, id));
        return NoContent();
    }
}
