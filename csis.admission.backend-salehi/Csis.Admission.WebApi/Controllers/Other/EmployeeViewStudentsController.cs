using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.ViewLogs.Dtos;
using Csis.Admission.Application.Features.EmployeeViewStudentLogs.Queries;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>گزارش طلابی که کارمند مشاهده کرده</summary>
[Route("/api/private/employee-view-student-logs")]
public sealed class EmployeeViewStudentsController : ApiControllerBase
{
    /// <summary>لیست</summary>
    [HttpGet,CsisAuthorize]
    public async Task<ActionResult<Result<EmployeeViewStudentLogDto[]>>> Get() {
        return OkResult(await Mediator.Send(new GetEmployeeViewStudentLogByPersonnelIdQuery()));
    }

    /// <summary>لیست آخرین</summary>
    [HttpGet("last"), CsisAuthorize]
    public async Task<ActionResult<Result<EmployeeLastViewStudentLogDto[]>>> GetLast() {
        return OkResult(await Mediator.Send(new EmployeeLastViewStudentLogDtoByPersonnelIdQuery()));
    }
}
