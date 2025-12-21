using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.TeachGrades.Dtos;
using Csis.Admission.Application.Features.TeachGrades.Queries;
using Csis.Admission.Application.Features.TeachGrades.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>رتبه تدریس</summary>
[Route("/api/private/teach-grades")]
public sealed class StudentTeachGradesController : ApiControllerBase
{
    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentTeachGradeView)]
    public async Task<ActionResult<Result<TeachGradeDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetTeachGradeByIdQuery(id)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentTeachGradeRegister)]
    public async Task<ActionResult<Result<TeachGradeDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetTeachGradesByCodmQuery(codm)));
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentTeachGradeRegister)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateTeachGradeCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentTeachGradeRegister)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTeachGradeCommand command) {
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
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentTeachGradeRegister, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteTeachGradeRequestCommand(codm, id));
        return NoContent();
    }
}
