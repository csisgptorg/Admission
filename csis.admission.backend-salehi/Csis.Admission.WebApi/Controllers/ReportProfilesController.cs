/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Features.ReportProfiles.Commands;
using Csis.Admission.Application.Features.ReportProfiles.Dtos;
using Csis.Admission.Application.Features.ReportProfiles.Queries;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// کنترلر مدیریت پروفایل گزارشات
/// </summary>
[Route("/api/private/report-profiles")]
[CsisAuthorize]
public sealed class ReportProfilesController : ApiControllerBase
{
    /// <summary>
    /// دریافت لیست پروفایل های ذخیره شده برای گزارش
    /// </summary>
    /// <param name="reportType">نوع گزارش</param>
    /// <returns></returns>
    [HttpGet("list/{reportType:min(0)}")]
    public async Task<ActionResult<Result<List<ReportProfileDto>>>> GetProfiles([FromRoute] ReportProfileType reportType) {
        return OkResult(await Mediator.Send(new GetReportProfilesListQuery(reportType)));
    }

    /// <summary>
    /// فراخوانی ساختار پروفایل گزارش
    /// </summary>
    /// <param name="id">شناسه پروفایل</param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}")]
    public async Task<ActionResult<Result<ReportProfileStructure>>> GetProfileStructure([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetProfileStructureByIdQuery(id)));
    }

    /// <summary>
    /// ایجاد پروفایل گزارش جدید
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReportProfileCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetProfileStructure), new { id = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// ویرایش پروفایل گزارش
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateReportProfileCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف پروفایل گزارش
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <returns></returns>
    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        await Mediator.Send(new DeleteReportProfileCommand(id));
        return NoContent();
    }
}
