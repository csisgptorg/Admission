using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.SoldierStudents.Dtos;
using Csis.Admission.Application.Features.SoldierStudents.Queries;
using Csis.Admission.Application.Features.SoldierStudents.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>سرباز طلبه</summary>
[Route("/api/private/soldier-students")]
public sealed class StudentSoldiersController : ApiControllerBase
{
    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentSoldierView)]
    public async Task<ActionResult<Result<SoldierStudentDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetSoldierStudentByIdQuery(id)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentSoldierView)]
    public async Task<ActionResult<Result<List<SoldierStudentDto>>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetSoldierStudentByCodmQuery(codm)));
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentSoldierRegister)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateSoldierStudentCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentSoldierRegister)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSoldierStudentCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentSoldierRegister)]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        await Mediator.Send(new DeleteSoldierStudentCommand(id));
        return NoContent();
    }
}
