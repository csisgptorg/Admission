using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Rurals.Dtos;
using Csis.Admission.Application.Features.Rurals.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>
/// دریافت لیست دهستان ها بر اساس استان
/// </summary>
[Route("/api/public/rurals"), Tags("Rurals")]
public sealed class RuralsControllerPublic : ApiControllerBase
{
    /// <summary>لیست دهستان ها</summary>
    [HttpGet]
    public async Task<ActionResult<Result<RuralDto[]>>> GetRurals([FromQuery] short? PortionId) {
        return OkResult(await Mediator.Send(new GetRuralsQuery(PortionId)));
    }
}
