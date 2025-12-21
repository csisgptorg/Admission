using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.EliteLevels.Dtos;
using Csis.Admission.Application.Features.EliteLevels.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>سطح نخبگانی</summary>
[Route("/api/private/elite-levels")]
public sealed class EliteLevelsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<EliteLevelDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetEliteLevelsQuery()));
    }
}
