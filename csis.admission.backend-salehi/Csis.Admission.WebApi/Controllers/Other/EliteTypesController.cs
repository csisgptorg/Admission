using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.EliteTypes.Dtos;
using Csis.Admission.Application.Features.EliteTypes.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>نوع نخبگانی</summary>
[Route("/api/private/elite-types")]
public sealed class EliteTypesController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize]
    public async Task<ActionResult<Result<EliteTypeDto[]>>> GetAll() {
        return OkResult(await Mediator.Send(new GetEliteTypesQuery()));
    }
}
