using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Features.ReportBuilders.Dtos;
using Csis.Admission.Application.Features.ReportBuilders.Queries;
using Csis.Admission.Application.Features.ReportBuilders.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>گزارش ساز</summary>
[Route("api/private/report-builder")]
public sealed class ReportBuilderController : ApiControllerBase
{
    /// <summary>گزارش</summary>
    [HttpPost("report"), CsisAuthorize(PermissionsEnum.ReportBuilderReport)]
    public async Task<ActionResult> Report(ReportBuilderQuery query) {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>خرجی اکسل گزارش</summary>
    [HttpPost("report/excel"), CsisAuthorize(PermissionsEnum.ReportBuilderReport)]
    public async Task<ActionResult> ReportExcel(ReportBuilderToExcelQuery query) {
        var result = await Mediator.Send(query);
        return File(result.FileByte,result.MIMEType,result.FileName);
    }

    /// <summary>جداول</summary>
    [HttpGet("tables"), CsisAuthorize(PermissionsEnum.ReportBuilderTables)] 
    public async Task<ActionResult<Result<ReportBuilderModel.Table[]>>> GetTables() {
        return OkResult(await Mediator.Send(new ReportBuilderTablesQuery()));
    }

    /// <summary>ثبت</summary>
    [HttpPost, CsisAuthorize(PermissionsEnum.ReportBuilderCreate)]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateReportBuilderCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<long>.Success(result));
    }

    /// <summary>بروز رسانی</summary>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.ReportBuilderUpdate)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReportBuilderCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>حذف</summary>
    [HttpDelete("{id:min(1)}"), CsisAuthorize(PermissionsEnum.ReportBuilderDelete)]
    public async Task<IActionResult> Delete([FromRoute] long id) {
        await Mediator.Send(new DeleteReportBuilderCommand(id));
        return NoContent();
    }

    /// <summary>لیست</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.ReportBuildersView)]
    public async Task<ActionResult<Result<ReportBuilderTitleDto>>> GetAll() {
        var result = await Mediator.Send(new GetReportBuildersQuery());
        return OkResult(result);
    }

    /// <summary>دریافت با شناسه</summary>
    [HttpGet("{id:min(1)}"), CsisAuthorize(PermissionsEnum.ReportBuildersView)]
    public async Task<ActionResult<Result<ReportBuilderDto>>> GetById([FromRoute] long id) {
        return OkResult(await Mediator.Send(new GetReportBuilderByIdQuery(id)));
    }
}
