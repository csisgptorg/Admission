using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.PreachGrades.Dtos;
using Csis.Admission.Application.Features.PreachGrades.Queries;
using Csis.Admission.Application.Features.PreachGrades.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// رتبه تبلیغی
/// </summary>
[Route("/api/private/preach-grades")]
public sealed class StudentPreachGradesController : ApiControllerBase
{
    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentPreachGradeView)]
    public async Task<ActionResult<Result<PreachGradeDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetPreachGradeByIdQuery(id)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentPreachGradeView)]
    public async Task<ActionResult<Result<PreachGradeDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetPreachGradesByCodmQuery(codm)));
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentPreachGradeRegister)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreatePreachGradeCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentPreachGradeRegister)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePreachGradeCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="codm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentPreachGradeRegister,PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeletePreachGradeRequestCommand(codm, id));
        return NoContent();
    }
}
