using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Elites.Dtos;
using Csis.Admission.Application.Features.Elites.Queries;
using Csis.Admission.Application.Features.Elites.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>نخبگان</summary>
[Route("/api/private/elites"),Tags("StudentElites")]
public sealed class StudentElitesController : ApiControllerBase
{
    /// <summary>دریافت لیست نخبگان</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentEliteView)]
    public async Task<ActionResult<List<EliteDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetElitesByCodmQuery(codm)));
    }

    /// <summary>دریافت نخبگان بر اساس شناسه</summary>
    [HttpGet("{id}"), CsisAuthorize(PermissionsEnum.StudentEliteView)]
    public async Task<ActionResult<EliteDto>> GetById(int id) {
        return OkResult(await Mediator.Send(new GetElitesByIdQuery(id)));
    }

    /// <summary>ثبت نخبگان</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentEliteCreateOrUpdate)]
    public async Task<IActionResult> Create([FromBody] CreateEliteRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>بروزرسانی نخبگان</summary>
    [HttpPut, CsisAuthorize(PermissionsEnum.StudentEliteCreateOrUpdate)]
    public async Task<IActionResult> Update([FromBody] UpdateEliteRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف نخبگان</summary>
    [HttpDelete, CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.StudentEliteDelete)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteEliteRequestCommand(codm, id));
        return NoContent();
    }
}
