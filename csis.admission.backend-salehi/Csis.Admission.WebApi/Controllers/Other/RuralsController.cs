using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Rurals.Dtos;
using Csis.Admission.Application.Features.Rurals.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>
/// دریافت لیست دهستان ها بر اساس استان
/// </summary>
[Route("/api/private/rurals")]
public sealed class RuralsController : ApiControllerBase
{
    /// <summary>لیست دهستان ها</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<RuralDto[]>>> GetRurals([FromQuery] short? PortionId) {
        return OkResult(await Mediator.Send(new GetRuralsQuery(PortionId)));
    }
}
