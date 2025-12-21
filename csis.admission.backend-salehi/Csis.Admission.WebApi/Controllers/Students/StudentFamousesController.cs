using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Famouses.Commands;
using Csis.Admission.Application.Features.Famouses.Dtos;
using Csis.Admission.Application.Features.Famouses.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>مشهور</summary>
[Route("/api/private/student/famouses")]
public sealed class StudentFamousesController : ApiControllerBase
{
    /// <summary>
    /// دریافت مشهور با شناسه
    /// </summary>
    /// <param name="id">شناسه مشهور</param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentFamousView)]
    public async Task<ActionResult<Result<StudentFamousDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetFamousByIdQuery(id)));
    }

    /// <summary>
    /// دریافت مشهور با شناسه
    /// </summary>
    /// <param name="codm">شناسه مشهور</param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentFamousView)]
    public async Task<ActionResult<Result<StudentFamousDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetFamousByCodmQuery(codm)));
    }

    /// <summary>
    /// ایجاد مشهور جدید
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentFamousCreate)]
    public async Task<ActionResult<Result<long>>> Create([FromBody] CreateFamousRequestCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<long>.Success(result));
    }

    /// <summary>
    /// ویرایش مشهور
    /// </summary>
    /// <param name="id">شناسه مشهور</param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentFamousEdit)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFamousRequestCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف مشهور
    /// </summary>
    /// <param name="codm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentFamousDelete, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteFamousRequestCommand(codm, id));
        return NoContent();
    }
}
