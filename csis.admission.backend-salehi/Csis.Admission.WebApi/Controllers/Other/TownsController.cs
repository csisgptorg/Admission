using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Towns.Dtos;
using Csis.Admission.Application.Features.Towns.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>
/// دریافت لیست شهرستان ها بر اساس استان
/// </summary>
[Route("/api/private/towns")]
public sealed class TownsController : ApiControllerBase
{
    /// <summary>لیست شهرستان ها</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<TownDto[]>>> GetTowns([FromQuery] short? PortionId) {
        return OkResult(await Mediator.Send(new GetTownsQuery(PortionId)));
    }
}
