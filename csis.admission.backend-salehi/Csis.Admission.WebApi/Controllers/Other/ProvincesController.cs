using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Provinces.Dtos;
using Csis.Admission.Application.Features.Provinces.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>استان</summary>
[Route("/api/private/provinces")]
public sealed class ProvincesController : ApiControllerBase
{
    /// <summary>دریافت لیست استانها</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<List<ProvinceDto>>>> GetAll() {
        return OkResult(await Mediator.Send(new GetProvincesQuery()));
    }
}
