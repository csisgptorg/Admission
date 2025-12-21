using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Features.Elites.Dtos;
using Csis.Admission.Application.Features.Elites.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>نخبگان</summary>
[Route("/api/public/student-elites"), Tags("StudentElites")]
public sealed class StudentElitesPublicController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<List<EliteDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetElitesByCodmQuery(codm)));
    }

    /// <summary>دریافت نخبگان بر اساس شناسه</summary>
    [HttpGet("{id}"), CsisAuthorizeStudent]
    public async Task<ActionResult<EliteDto>> GetById(int id) {
        return OkResult(await Mediator.Send(new GetElitesByIdQuery(id)));
    }
}
