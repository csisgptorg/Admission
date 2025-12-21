using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Preaches.Dtos;
using Csis.Admission.Application.Features.Preaches.Queries;
using Csis.Admission.Application.Features.Preaches.Commands;
using Csis.Admission.WebApi.Filters;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>تبلیغ</summary>
[Route("/api/private/preaches"), Tags("Preaches")]
public sealed class StudentPreachesController : ApiControllerBase
{
    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentPreachView)]
    public async Task<ActionResult<Result<PreachDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetPreachByIdQuery(id)));
    }

    /// <summary>دریافت</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentPreachView)]
    public async Task<ActionResult<Result<PreachDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetPreachesByCodmQuery(codm)));
    }


    /// <summary>ثبت</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentPreachRegister)]
    public async Task<ActionResult<Result<long>>> Create([FromBody] CreatePreachRequestCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<long>.Success(result));
    }


    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<ActionResult<Result<int>>> DataImport([FromBody] DataImportPreachCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentPreachRegister)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePreachRequestCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <returns></returns>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentPreachRegister, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeletePreachRequestCommand(codm, id));
        return NoContent();
    }
}
