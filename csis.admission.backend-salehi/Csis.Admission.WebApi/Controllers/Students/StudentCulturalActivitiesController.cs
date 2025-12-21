using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.CulturalActivities.Dtos;
using Csis.Admission.Application.Features.CulturalActivities.Queries;
using Csis.Admission.Application.Features.CulturalActivities.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>فعالیت فرهنگی طلبه</summary>
[Route("/api/private/cultural-activities"), Tags("StudentCulturalActivities")]
public sealed class StudentCulturalActivitiesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentCulturalActivityView)]
    public async Task<ActionResult<Result<CulturalActivityDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetCulturalActivityByIdQuery(id)));
    }

    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentCulturalActivityView)]
    public async Task<ActionResult<Result<CulturalActivityDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetCulturalActivitiesByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentCulturalActivityCreate)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateCulturalActivityCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<int>.Success(result));
    }

    /// <inheritdoc/>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentCulturalActivityUpdate)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCulturalActivityCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <inheritdoc/>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentCulturalActivityDelete, PermissionsEnum.SeniorPersonnel)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteCulturalActivityRequestCommand(codm, id));
        return NoContent();
    }
}
