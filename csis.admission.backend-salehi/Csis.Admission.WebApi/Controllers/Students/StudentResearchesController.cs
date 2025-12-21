using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Researches.Dtos;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Researches.Commands;
using Csis.Admission.Application.Features.Researches.Queries;

namespace Csis.Admission.WebApi.Controllers;
/// <summary>پژوهش</summary>
[Route("/api/private/researches")]
public sealed class StudentResearchesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentResearchView)]
    public async Task<ActionResult<Result<ResearchDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetResearchesByCodmQuery(codm)));
    }

    /// <summary>
    /// دریافت پژوهش بر اساس شناسه
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}"), CsisAuthorize(PermissionsEnum.StudentResearchView)]
    public async Task<ActionResult<Result<ResearchDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetResearchByIdQuery(id)));
    }

    /// <summary>
    /// ثبت پژوهش
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentResearchCreate)]
    public async Task<ActionResult<Result<long>>> Create([FromBody] CreateResearchRequestCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<long>.Success(result));
    }

    /// <summary>
    /// ویرایش پژوهش
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut, CsisAuthorize(PermissionsEnum.StudentResearchEdit)]
    public async Task<ActionResult> Update([FromBody] UpdateResearchRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف پژوهش
    /// </summary>
    /// <param name="codm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentResearchDelete, PermissionsEnum.SeniorPersonnel)]
    public async Task<ActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteResearchRequestCommand(codm, id));
        return NoContent();
    }
}
