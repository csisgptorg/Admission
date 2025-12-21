using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Branches.Dtos;
using Csis.Admission.Application.Features.Branches.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>شعب</summary>
[Route("/api/private/branches")]
public sealed class BranchesController : ApiControllerBase
{
    /// <summary>دریافت لیست شعب</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<List<BranchDto>>>> GetAll([FromQuery]bool? hasAgency) {
        return OkResult(await Mediator.Send(new GetBranchesQuery(hasAgency)));
    }

    /// <summary>دریافت لیست نمایندگی های شعبه خاص</summary>
    [HttpGet("{id}/agencies"), CsisAuthorize]
    public async Task<ActionResult<Result<List<BranchDto>>>> GetAll(short id) {
        return OkResult(await Mediator.Send(new GetAgenciesByBranchIdQuery(id)));
    }
}
