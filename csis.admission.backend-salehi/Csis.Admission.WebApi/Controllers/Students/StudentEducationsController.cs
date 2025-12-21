using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Educations.Dtos;
using Csis.Admission.Application.Features.Educations.Queries;
using Csis.Admission.Application.Features.Educations.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>تحصیل طلبه</summary>
[Route("/api/private/educations")]
public sealed class StudentEducationsController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentEducationView)]
    public async Task<ActionResult<EducationDto>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetEducationByCodmQuery(codm)));
    }

    /// <summary>
    /// به‌روزرسانی تحصیلات حوزوی طلبه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut, CsisAuthorize(PermissionsEnum.StudentEducationEdit)]
    public async Task<ActionResult> Update([FromBody] UpdateEducationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
