using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.Addresses.Dtos;
using Csis.Admission.Application.Features.Addresses.Queries;
using Csis.Admission.Application.Features.Addresses.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>آدرس</summary>
[Route("/api/public/student/addresses"), Tags("StudentAddresses"), CsisAuthorizeStudent]
public sealed class StudentAddressesPublicController : ApiControllerBase
{
    private readonly ICsisWsmService _wsmService;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentAddressesPublicController(ICsisWsmService wsmService,ICsisAuthenticatedUserService authenticatedUserService) {
        _wsmService = wsmService;
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>ثبت</summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromQuery] bool? confirmed,[FromBody] CreateOrUpdateStudentAddressRequestCommand command) {
        command.Confirmed = confirmed;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <inheritdoc/>
    [HttpGet]
    public async Task<ActionResult<Result<AddressDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetAddressesByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpGet("postal-code/{postalCode}")]
    public async Task<ActionResult<AddressModel>> GetAddressByPostalCode(long postalCode) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        var address = await _wsmService.GetAddressByPostalCode(codm, postalCode, default);
        return OkResult(address.GetAddress(codm, postalCode));
    }

    /// <inheritdoc/>
    [HttpPut("confirm")]
    public async Task<IActionResult> Confirm() {
        await Mediator.Send(new ConfirmStudentAddressCommand());
        return NoContent();
    }
}
