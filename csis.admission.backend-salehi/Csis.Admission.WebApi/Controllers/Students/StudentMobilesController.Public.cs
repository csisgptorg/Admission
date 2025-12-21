using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.StudentMobiles.Commands;
using Csis.Admission.Application.Features.StudentMobiles.Dtos;
using Csis.Admission.Application.Features.StudentMobiles.Queries;
using Csis.Authorization;
using Csis.Authorization.Services;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>موبایل طلبه</summary>
[Route("/api/public/students/mobiles"), Tags("StudentMobiles")]
public sealed class StudentMobilesController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentMobilesController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>دریافت موبایل های خانواده</summary>
    /// <returns></returns>
    [HttpGet, CsisAuthorizeStudent]
    public async Task<ActionResult<Result<FamilyMobileDto[]>>> GetByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetFamilyMobilesByCodmQuery(codm)));
    }

    /// <summary>به روز رسانی موبایل</summary>
    [HttpPut, CsisAuthorizeStudent]
    public async Task<IActionResult> Update([FromBody] UpdateStudentMobileRequestCommandAction command, [FromQuery] string otp) {
        await Mediator.Send(new UpdateStudentPhoneRequestCommand(Codm: null, command.Mobile, command.PreCodeTel, command.Tel, otp, Confirm: false));
        return NoContent();
    }

    /// <summary>به روز رسانی موبایل تکفل</summary>
    [HttpPut("dependent"), CsisAuthorizeStudent]
    public async Task<IActionResult> UpdateDependent([FromBody] UpdateDependentMobileRequestCommandAction command, [FromQuery] string otp) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        await Mediator.Send(new UpdateDependentMobileRequestCommand { Codm = codm, DependentId = command.DependentId, Mobile = command.Mobile, Otp = otp });
        return NoContent();
    }

}
