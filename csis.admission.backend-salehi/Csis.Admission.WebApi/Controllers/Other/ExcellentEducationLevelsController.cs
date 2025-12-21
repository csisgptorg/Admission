using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.ExcellentEducationLevels.Dtos;
using Csis.Admission.Application.Features.ExcellentEducationLevels.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>سال تحصیلی ممتازین</summary>
[Route("/api/private/excellent-education-levels")]
public sealed class ExcellentEducationLevelsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<ExcellentEducationLevelDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetExcellentEducationLevelsQuery()));
    }
}
