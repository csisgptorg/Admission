using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Teaches.Commands;
using Csis.Admission.Application.Features.Teaches.Dtos;
using Csis.Admission.Application.Features.Teaches.Queries;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>تدریس</summary>
[Route("api/private/teaches"), Tags("Teaches")]
public sealed class StudentTeachesController : ApiControllerBase
{
    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentTeachView)]
    public async Task<ActionResult<Result<TeachDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetTeachByIdQuery(id)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentTeachView)]
    public async Task<ActionResult<Result<TeachDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetTeachesByCodmQuery(codm)));
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentTeachRegister)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateTeachCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentTeachRegister)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTeachCommand command) {
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
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentTeachRegister, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteTeachRequestCommand(codm, id));
        return NoContent();
    }

    /// <summary>
    /// ارتباط داده ای
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<ActionResult<Result<int>>> DataImport([FromBody] TeachDataImportCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }
}
