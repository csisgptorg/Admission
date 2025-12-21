using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.NonStudentDependants.Commands;
using Csis.Admission.Application.Features.NonStudentDependants.Dtos;
using Csis.Admission.Application.Features.NonStudentDependants.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// مدیریت موجودیت تکفل های غیرطلبه
/// </summary>
[Route("/api/private/non-student-dependants")]
public sealed class NonStudentDependantsController : ApiControllerBase
{
    /// <summary>
    /// جستجوی موجودیت تکفل های غیرطلبه
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("search"), CsisAuthorize]
    [DynamicSearch<NonStudentDependant>]
    public async Task<ActionResult<PaginatedResult<NonStudentDependantDto>>> Search([FromBody] SearchNonStudentDependantsQuery query) {
        var result = await Mediator.Send(query);
        return PaginatedResult(result);
    }

    /// <summary>
    /// دریافت موجودیت تکفل های غیرطلبه با شناسه
    /// </summary>
    /// <param name="id">شناسه موجودیت تکفل های غیرطلبه</param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize]
    public async Task<ActionResult<Result<NonStudentDependantDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetNonStudentDependantByIdQuery(id)));
    }

    /// <summary>
    /// ایجاد موجودیت تکفل های غیرطلبه جدید
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateNonStudentDependantCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id = result}, Result<int>.Success(result));
    }

    /// <summary>
    /// ویرایش موجودیت تکفل های غیرطلبه
    /// </summary>
    /// <param name="id">شناسه موجودیت تکفل های غیرطلبه</param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNonStudentDependantCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف موجودیت تکفل های غیرطلبه
    /// </summary>
    /// <param name="id">شناسه موجودیت تکفل های غیرطلبه</param>
    /// <returns></returns>
    [HttpDelete("{id:min(1)}"), CsisAuthorize]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        await Mediator.Send(new DeleteNonStudentDependantCommand(id));
        return NoContent();
    }
}
