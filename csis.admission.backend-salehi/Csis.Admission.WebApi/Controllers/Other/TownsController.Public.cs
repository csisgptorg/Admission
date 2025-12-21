using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Towns.Dtos;
using Csis.Admission.Application.Features.Towns.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>
/// دریافت لیست شهرستان ها بر اساس استان
/// </summary>
[Route("/api/public/towns"), Tags("Towns")]
public sealed class TownsControllerPublic : ApiControllerBase
{
    /// <summary>لیست شهرستان ها</summary>
    [HttpGet]
    public async Task<ActionResult<Result<TownDto[]>>> GetTowns([FromQuery] short? PortionId) {
        return OkResult(await Mediator.Send(new GetTownsQuery(PortionId)));
    }
}
