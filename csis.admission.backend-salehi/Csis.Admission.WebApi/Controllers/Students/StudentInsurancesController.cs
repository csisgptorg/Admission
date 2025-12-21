using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Insurances.Dtos;
using Csis.Admission.Application.Features.Insurances.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>بیمه</summary>
[Route("/api/private/student/insurances")]
public sealed class StudentInsurancesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentInsuranceView)]
    public async Task<ActionResult<StudentDependentInsuranceDto>> GetByCodm([FromQuery] int codm,long? dependentId) {
        return OkResult(await Mediator.Send(new GetStudentDependentInsurancesByCodmQuery(codm,dependentId)));
    }
}
