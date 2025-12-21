using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Protests.Dtos;
using Csis.Admission.Application.Features.Protests.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>اعتراضات</summary>
[Route("/api/private/protests")]
public sealed class StudentProtestsController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentPregnancyView)]
    public async Task<ActionResult<List<ProtestDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetProtestsByCodmQuery(codm)));
    }
}
