using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Divorce.Commands;
using Csis.Admission.Application.Features.Marriages.Dtos;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت موجودیت ازدواج
/// </summary>
[Route("/api/private/divorces"), Tags("Divorces")]
public sealed class StudentDevorcesController(ICsisWsmService csisWsmService) : ApiControllerBase
{

    /// <summary>
    /// ثبت طلاق طلاب خواهر 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/sister"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<IActionResult> DataImportDivorce([FromBody] UpdateStudentSisterDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// ثبت طلاق تکفل
    /// </summary>
    /// <returns></returns>
    [HttpPost("dependent"), CsisAuthorize(PermissionsEnum.StudentDivorceRegister)]
    public async Task<IActionResult> CreateDivorceSupervisor([FromBody] UpdateDependentDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق همسر
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("wife")]
    public async Task<IActionResult> CreateDivorceWife([FromBody] UpdateWifeDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// ارتباط داده ای - طلاق سرپرست 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/sister/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<IActionResult> DataImportDivorce([FromBody] UpdateStudentSisterDivorceDataImportCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ارتباط داده ای - طلاق تکفل 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<IActionResult> DataImportDependentDivorce([FromBody] UpdateDependentDivorceDataImportCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// دریافت خلاصه تجمیعی
    /// </summary>
    /// <param name="nationalCode"></param>
    /// <returns></returns>
    [HttpGet("GetTajmieiSummary"), CsisAuthorize()]
    public async Task<IActionResult> GetTajmieiSummary([FromQuery] string nationalCode) {
        var result = await csisWsmService.GetTajmieiSummary(nationalCode, CancellationToken.None);
        return Ok(result);
    }
}
