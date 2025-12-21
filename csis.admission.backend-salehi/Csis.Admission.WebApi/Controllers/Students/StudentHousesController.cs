using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Houses.Dtos;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Houses.Queries;
using Csis.Admission.Application.Features.Houses.Commands;
using Csis.Admission.Application.Features.Students.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>مسکن</summary>
[Route("/api/private/houses")]
public sealed class StudentHousesController : ApiControllerBase
{
    /// <summary>دریافت درخواست با شناسه</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentHouseView)]
    public async Task<ActionResult<Result<HouseDto>>> GetByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetHouseByCodmQuery(codm)));
    }

    /// <summary>ایجاد یا ویرایش درخواست مسکن</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentHouseRegister)]
    public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateHouseEmployeeRequestCommand command) {
        command.Confirmed=true;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف اطلاعات مسکن</summary>
    [HttpDelete, CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.StudentHouseDelete)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteHouseRequestCommand(codm, id));
        return NoContent();
    }

    /// <summary>دریافت سابقه مسکن</summary>
    [HttpGet("history"), CsisAuthorize(PermissionsEnum.StudentHouseHistoryView)]
    public async Task<ActionResult<Result<StudentHouseHistoryDto>>> HouseHistory([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentHouseHistoryByCodmQuery(codm)));
    }
}
