using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Marriages.Commands;
using Csis.Admission.Application.Features.Marriages.Dtos;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Application.Features.StudentDependents.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت موجودیت ازدواج
/// </summary>
[Route("/api/private/marriages"), Tags("Marriages")]
public sealed class MarriagesController : ApiControllerBase
{ 
    /// <summary>
    /// دریافت موجودیت ازدواج با شناسه
    /// </summary>
    /// <param name="id">شناسه موجودیت ازدواج</param>
    /// <returns></returns>
    [HttpGet("person/{id:min(1)}"), CsisAuthorize(PermissionsEnum.StudentMarriageView)]
    public async Task<ActionResult<Result<MarriageDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetMarriageByIdQuery(id)));
    }

    /// <summary>
    /// دریافت مشخصات افراد تحت تکفل - همسران 
    /// </summary>
    /// <returns></returns>
    [HttpGet("dependent/spouses/{codm:min(1)}"), CsisAuthorize(PermissionsEnum.StudentMarriageView)]
    public async Task<IActionResult> GetMaleSpousesInfo([FromRoute] int codm) {
        var result = await Mediator.Send(new GetDependentSpousesQuery(codm));
        return OkResult(result);
    }

    /// <summary>
    /// جستجوی موجودیت ازدواج
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("person/search"), CsisAuthorize(PermissionsEnum.StudentMarriageView)]
    [DynamicSearch<Marriage>]
    public async Task<ActionResult<PaginatedResult<MarriageDto>>> Search([FromBody] SearchMarriagesQuery query) {
        var result = await Mediator.Send(query);
        return PaginatedResult(result);
    }


    /// <summary>
    /// ثبت ازدواج طلاب خواهر 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/sister"), CsisAuthorize(PermissionsEnum.StudentMarriageRegister)]
    public async Task<ActionResult<SpouseMarriageDto>> UpdateStudentSisterMarriage([FromBody] UpdateStudentSisterMarriageRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت ازدواج تکفل 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent"), CsisAuthorize(PermissionsEnum.DependentMarriageRegister)]
    public async Task<IActionResult> UpdateDependentMarriage([FromBody] UpdateChildMarriageRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }



    /// <summary>
    /// ارتباط داده ای - ازدواج سرپرست
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/sister/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<IActionResult> DataImport([FromBody] MarriageDataImportCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ارتباط داده ای - ازدواج تکفل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<IActionResult> DataImportDependent([FromBody] UpdateChildMarriageCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }



}
