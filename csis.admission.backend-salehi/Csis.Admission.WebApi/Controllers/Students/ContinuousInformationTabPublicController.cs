using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.ContinuousInformationTabs;
using Csis.Authorization;
using Csis.Authorization.Services;
using Csis.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// نمایش محتوای تب مشخصات مستمری
/// </summary>

[Route("api/public/continuous-information-tab"),Tags("ContinuousInformationTab"), CsisAuthorizeStudent]
public class ContinuousInformationTabPublicController: ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _userService;

    /// <inheritdoc/>>
    public ContinuousInformationTabPublicController(ICsisAuthenticatedUserService userService) => _userService = userService;

    /// <inheritdoc/>
    [HttpGet]
    public async Task<ActionResult<Result<string>>> GetsByCodm() {
        var codm = await _userService.GetStudentCodmAsync();
        return OkResult(await Mediator.Send(new GetContinuousInformationTabQuery(codm.ToInt())));
    }
}
