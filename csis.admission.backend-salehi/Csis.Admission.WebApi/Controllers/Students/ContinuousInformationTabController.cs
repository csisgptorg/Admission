using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.ContinuousInformationTabs;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// نمایش محتوای تب مشخصات مستمری
/// </summary>
[Route("api/private/continuous-information-tab"), CsisAuthorize(PermissionsEnum.ViewContinuousInformationTab)]
public class ContinuousInformationTabController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet("{codm:min(1)}")]
    public async Task<ActionResult<Result<string>>> GetsByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetContinuousInformationTabQuery(codm, true)));
    }
}
