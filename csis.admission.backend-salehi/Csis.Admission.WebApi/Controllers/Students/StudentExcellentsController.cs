using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Excellents.Dtos;
using Csis.Admission.Application.Features.Excellents.Queries;
using Csis.Admission.Application.Features.Excellents.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>ممتازی</summary>
[Route("/api/private/excellents")]
public sealed class StudentExcellentsController : ApiControllerBase
{
    /// <summary>دریافت لیست ممتازی</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentExcellentView)]
    public async Task<ActionResult<Result<List<ExcellentDto>>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetExcellentsByCodmQuery(codm)));
    }

    /// <summary>حذف ممتازی</summary>
    [HttpDelete, CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.StudentExcellentDelete)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteExcellentRequestCommand(codm, id));
        return NoContent();
    }
}
