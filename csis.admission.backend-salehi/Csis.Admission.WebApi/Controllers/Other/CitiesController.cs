using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Cities.Dtos;
using Csis.Admission.Application.Features.Cities.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>شهرستان</summary>
[Route("/api/private/cities")]
public sealed class CitiesController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.CityView)]
    public async Task<ActionResult<Result<CityDto[]>>> GetAll([FromQuery] short? ProvinceId) {
        return OkResult(await Mediator.Send(new GetCitiesQuery(ProvinceId)));
    }
}
