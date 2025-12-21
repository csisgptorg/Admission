using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Branches.Dtos;
using Csis.Admission.Application.Features.Branches.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>شعب</summary>
[Route("/api/private/agencies")]
public sealed class AgenciesController : ApiControllerBase
{
    /// <summary>دریافت لیست نمایندگی های شعبه خاص</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<List<BranchDto>>>> GetAll([FromQuery]short BranchId) {
        return OkResult(await Mediator.Send(new GetAgenciesByBranchIdQuery(BranchId)));
    }
}
