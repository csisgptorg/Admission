using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.ExcellentEducationYears.Dtos;
using Csis.Admission.Application.Features.ExcellentEducationYears.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>سال تحصیلی ممتازین</summary>
[Route("/api/private/excellent-education-years")]
public sealed class ExcellentEducationYearsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<ExcellentEducationYearDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetExcellentEducationYearsQuery()));
    }
}
