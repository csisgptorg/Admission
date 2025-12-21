using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Schools.Dtos;
using Csis.Admission.Application.Features.Schools.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>مدرسه</summary>
[Route("/api/private/schools")]
public sealed class SchoolsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<SchoolDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetSchoolsQuery()));
    }
}
