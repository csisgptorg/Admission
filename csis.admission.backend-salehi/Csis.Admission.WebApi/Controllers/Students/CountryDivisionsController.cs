using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.CountryDivisions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// تقسیمات کشوری
/// </summary>
[Route("api/country-divisions")]
public class CountryDivisionsController : ApiControllerBase
{
    /// <summary>
    /// ایجاد شهر 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("set-town")]
    public async Task<ActionResult<Result<ProcedureResultDto>>> CreateSetTown([FromBody] CreateSetTownInCountryDivisionsCommand command) {
        return OkResult(await Mediator.Send(command));
    }
    
    /// <summary>
    /// ایجاد دهستان 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("set-rural")]
    public async Task<ActionResult<Result<ProcedureResultDto>>> CreateSetTown([FromBody] CreateSetRuralCountryDivisionsCommand command) {
        return OkResult(await Mediator.Send(command));
    }
    
    
    /// <summary>
    /// ایجاد بخش 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("set-portion")]
    public async Task<ActionResult<Result<ProcedureResultDto>>> CreateSetTown([FromBody] CreateSetPortionCountryDivisionsCommand command) {
        return OkResult(await Mediator.Send(command));
    }
}
