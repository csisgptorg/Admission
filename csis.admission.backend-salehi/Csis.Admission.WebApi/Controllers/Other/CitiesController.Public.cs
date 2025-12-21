using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Cities.Dtos;
using Csis.Admission.Application.Features.Cities.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;
/// <summary>
/// شهرها
/// </summary>
[Route("api/public/cities"),Tags("Cities")]
public class CitiesControllerPublic : ApiControllerBase
{
    /// <summary>لیست شهرها</summary>
    [HttpGet]
    public async Task<ActionResult<Result<CityDto[]>>> GetCities([FromQuery] short? ProvinceId) {
        return OkResult(await Mediator.Send(new GetCitiesQuery(ProvinceId)));
    }
}
