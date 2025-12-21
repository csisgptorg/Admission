using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Family.Dtos;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Persons;

/// <summary>
/// Persons
/// </summary>
[Route("/api/private/family")]
public sealed class FamilyController : ApiControllerBase
{
    /// <summary>
    /// نمایش اطلاعات طلاب و افراد تحت تکفل بر اساس کد ملی
    /// </summary>
    [HttpGet("national-code/{nationalCode}"), CsisAuthorizeApiKey(PermissionsEnum.HealthInsuranceApplication), ApiKeyHeader]
    public async Task<ActionResult<Result<List<HealthInsuranceFamilyDto>>>> GetFamilyByNationalCode([FromRoute] string nationalCode) {
        return OkResult(await Mediator.Send(new GetFamilyByNationalCodeQuery(nationalCode)));
    }
    /// <summary>
    /// نمایش اطلاعات طلاب و افراد تحت تکفل ایشان بر اساس کد یکتا
    /// </summary>
    [HttpGet("yekta-code/{yektaCode}"), CsisAuthorizeApiKey(PermissionsEnum.HealthInsuranceApplication), ApiKeyHeader]
    public async Task<ActionResult<Result<List<HealthInsuranceFamilyDto>>>> GetFamilyByYektaCode([FromRoute] string yektaCode) {
        return OkResult(await Mediator.Send(new GetFamilyByYektaCodeQuery(yektaCode)));
    }

}
