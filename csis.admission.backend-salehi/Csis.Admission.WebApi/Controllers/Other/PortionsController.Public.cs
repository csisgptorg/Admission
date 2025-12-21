using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Portions.Dtos;
using Csis.Admission.Application.Features.Portions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>بخش</summary>
[Route("/api/public/portions"),Tags("Portions")]
public sealed class PortionsControllerPublic : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet]
    public async Task<ActionResult<Result<PortionDto[]>>> GetAll([FromQuery] short? CityId) {
        return OkResult(await Mediator.Send(new GetPortionsQuery(CityId)));
    }
}
