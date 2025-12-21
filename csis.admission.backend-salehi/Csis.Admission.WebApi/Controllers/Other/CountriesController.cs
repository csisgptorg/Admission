using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Countries.Dtos;
using Csis.Admission.Application.Features.Countries.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>کشور</summary>
[Route("/api/private/countries")]
public sealed class CountriesController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.CityView)]
    public async Task<ActionResult<Result<CountryDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetCountriesQuery()));
    }
}
