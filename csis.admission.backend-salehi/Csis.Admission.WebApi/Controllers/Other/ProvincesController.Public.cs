using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Provinces.Dtos;
using Csis.Admission.Application.Features.Provinces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>استان</summary>
[Route("/api/public/provinces"), Tags("Provinces")]
public sealed class ProvincesControllerPublic : ApiControllerBase
{
    /// <summary>دریافت لیست استانها</summary>
    [HttpGet]
    public async Task<ActionResult<Result<List<ProvinceDto>>>> GetAll() {
        return OkResult(await Mediator.Send(new GetProvincesQuery()));
    }
}
