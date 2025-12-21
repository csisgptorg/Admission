using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.CulturalActivityGrades.Commands;
using Csis.Admission.Application.Features.CulturalActivityGrades.Dtos;
using Csis.Admission.Application.Features.CulturalActivityGrades.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>رتبه بندی فعالیت فرهنگی</summary>
[Route("/api/private/cultural-activity-grades")]
public sealed class StudentCulturalActivityGradesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentCulturalActivityGradeView)]
    public async Task<ActionResult<Result<List<CulturalActivityGradeDto>>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetCulturalActivityGradesByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentCulturalActivityDelete, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteCulturalActivityGradesByCodmRequestCommand(codm, id));
        return NoContent();
    }
}
