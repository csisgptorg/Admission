using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Portions.Dtos;
using Csis.Admission.Application.Features.Portions.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>بخش</summary>
[Route("/api/private/portions")]
public sealed class PortionsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.PortionView)]
    public async Task<ActionResult<Result<PortionDto[]>>> GetAll([FromQuery] short? CityId) {
        return OkResult(await Mediator.Send(new GetPortionsQuery(CityId)));
    }
}
