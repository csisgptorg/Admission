using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Features.Houses.Dtos;
using Csis.Admission.Application.Features.Houses.Queries;
using Csis.Admission.Application.Features.Houses.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>مسکن</summary>
[Route("/api/public/houses"), Tags("StudentHouses")]
public sealed class HousesPublicController : ApiControllerBase
{
    /// <summary>دریافت</summary>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<HouseDto>> Get() {
        return OkResult(await Mediator.Send(new GetHouseByCodmQuery(Codm: null)));
    }

    /// <summary>ثبت و بروز رسانی</summary>
    [HttpPost, CsisAuthorizeStudent]
    public async Task<IActionResult> CreateOrUpdate([FromQuery] bool? confirmed, [FromBody] CreateOrUpdateHouseRequestCommand command) {
        command.Confirmed = confirmed;
        await Mediator.Send(command);
        return NoContent();
    }
}
