using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.SoldierStudents.Dtos;
using Csis.Admission.Application.Features.SoldierStudents.Queries;
using Csis.Admission.Application.Features.SoldierStudents.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// سرباز طلبه
/// </summary>
[Route("/api/public/soldier-students"),Tags("SoldierStudents"),]
public sealed class SoldierStudentsPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <summary>
    /// SoldierStudentsController
    /// </summary>
    /// <param name="authenticatedUserService"></param>
    public SoldierStudentsPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>
    /// GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorizeStudent]
    public async Task<ActionResult<Result<SoldierStudentDto>>> GetById([FromRoute] int id) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetSoldierStudentByIdQuery(id,codm)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<List<SoldierStudentDto>>>> GetByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetSoldierStudentByCodmQuery(codm)));
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateSoldierStudentCommand command) {
        command.Codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorizeStudent]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSoldierStudentCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        command.Codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:min(1)}"), CsisAuthorizeStudent]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        await Mediator.Send(new DeleteSoldierStudentCommand(id,codm));
        return NoContent();
    }
}
