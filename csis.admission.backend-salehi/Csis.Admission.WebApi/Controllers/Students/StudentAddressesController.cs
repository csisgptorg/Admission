using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Addresses.Commands;
using Csis.Admission.Application.Features.Addresses.Dtos;
using Csis.Admission.Application.Features.Addresses.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>آدرس</summary>
[Route("/api/private/student/addresses")]
public sealed class StudentAddressesController(ICsisWsmService _wsmService) : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentAddressView)]
    public async Task<ActionResult<AddressDto>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetAddressesByCodmQuery(codm)));
    }

    /// <summary>ثبت</summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate( [FromBody] CreateOrUpdateStudentAddressEmployeeRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <inheritdoc/>
    [HttpGet("postal-code")]
    public async Task<ActionResult<AddressModel>> GetAddressByPostalCode([FromQuery] int codm, [FromQuery] long postalCode) {

        var address = await _wsmService.GetAddressByPostalCode(codm, postalCode, default);
        return OkResult(address.GetAddress(codm, postalCode));
    }
}
