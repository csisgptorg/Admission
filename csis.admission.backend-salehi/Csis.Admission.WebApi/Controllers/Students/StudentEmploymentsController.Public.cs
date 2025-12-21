using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.DependentEmployments.Queries;
using Csis.Admission.Application.Features.Employments.Commands;
using Csis.Admission.Application.Features.Employments.Dtos;
using Csis.Admission.Application.Features.Employments.Queries;
using Csis.Admission.Application.Features.DependentEmployments.Dtos;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>شغل و درآمد</summary>
[Route("/api/public/employments"), Tags("StudentEmployments"),CsisAuthorizeStudent]
public sealed class StudentEmploymentsPublicController : ApiControllerBase
{
    /// <summary>بروز رسانی وضعیت اشتغال طلبه</summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromQuery] bool? confirmed,[FromBody] CreateOrUpdateStudentEmploymentRequestCommand command) {
        command.Confirmed = confirmed;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>تایید وضعیت اشتغال طلبه</summary>
    [HttpPut("confirm")]
    public async Task<IActionResult> Confirm() {
        await Mediator.Send(new ConfirmStudentEmploymentCommand());
        return NoContent();
    }

    /// <summary>بروز رسانی وضعیت اشتغال تکفل</summary>
    [HttpPost("dependent")]
    public async Task<IActionResult> Update([FromBody] CreateOrUpdateDependentEmploymentRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>تایید وضعیت اشتغال تکفل</summary>
    [HttpPut("dependent/confirm/{dependentId}")]
    public async Task<IActionResult> DependentConfirm(long dependentId) {
        await Mediator.Send(new ConfirmDependentEmploymentCommand(dependentId));
        return NoContent();
    }

    /// <summary>دریافت وضعیت اشتغال طلبه</summary>
    [HttpGet]
    public async Task<ActionResult<Result<StudentEmploymentDto>>> GetAllByCodm() {
        return OkResult(await Mediator.Send(new GetStudentEmploymentByCodmQuery(Codm:null)));
    }

    /// <summary>دریافت وضعیت اشتغال تکفل</summary>
    [HttpGet("dependent")]
    public async Task<ActionResult<Result<List<DependentEmploymentDto>>>> GetDependentsEmploymentByCodm() {
        return OkResult(await Mediator.Send(new GetDependentsEmploymentByCodmQuery(null)));
    }
}
