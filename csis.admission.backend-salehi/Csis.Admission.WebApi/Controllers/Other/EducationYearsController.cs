using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.EducationYears.Dtos;
using Csis.Admission.Application.Features.EducationYears.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>سال تحصیلی</summary>
[Route("/api/private/education-years")]
public sealed class EducationYearsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<EducationYearDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetEducationYearsQuery()));
    }
}
