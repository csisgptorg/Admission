using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.StudentMobiles.Dtos;
using Csis.Admission.Application.Features.StudentMobiles.Queries;
using Csis.Admission.Application.Features.StudentMobiles.Commands;

namespace Csis.Admission.WebApi.Controllers;
/// <summary>
/// موبایل طلبه
/// </summary>
[Route("/api/private/students/mobiles"), Tags("StudentMobiles")]
public sealed class StudentMobilesController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentMobilesController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>دریافت موبایل های خانواده</summary>
    /// <returns></returns>
    [HttpGet("{codm:min(1)}"), CsisAuthorize(PermissionsEnum.StudentMobileView)]
    public async Task<ActionResult<Result<FamilyMobileDto[]>>> GetByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetFamilyMobilesByCodmQuery(codm)));
    }

    /// <summary>به روز رسانی موبایل</summary>
    [HttpPut, CsisAuthorize(PermissionsEnum.StudentMobileEdit)]
    public async Task<IActionResult> Update([FromBody] UpdateStudentPhoneRequestCommand command) {
       var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>به روز رسانی موبایل تکفل</summary>
    [HttpPut("dependent"), CsisAuthorize(PermissionsEnum.StudentDependentMobileEdit)]
    public async Task<IActionResult> UpdateDependent([FromBody] UpdateDependentMobileRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
