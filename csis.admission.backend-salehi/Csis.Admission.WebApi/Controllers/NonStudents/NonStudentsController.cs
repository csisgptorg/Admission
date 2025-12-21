using Csis.Authorization;
using Csis.Abstractions.Results;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Admission.Application.Features.NonStudents.Dtos;
using Csis.Admission.Application.Features.NonStudents.Queries;
using Csis.Admission.Application.Features.NonStudents.Commands;

namespace Csis.Admission.WebApi.Controllers.NonStudents;

/// <summary>
/// مدیریت موجودیت غیر طلبه
/// </summary>
[Route("/api/private/non-students")]
public sealed class NonStudentsController : ApiControllerBase
{
    /// <summary>
    /// جستجوی موجودیت غیر طلبه
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("search"), CsisAuthorize]
    [DynamicSearch<NonStudent, long>]
    public async Task<ActionResult<PaginatedResult<NonStudentDto>>> Search([FromBody] SearchNonStudentsQuery query) {
        var result = await Mediator.Send(query);
        return PaginatedResult(result);
    }

    /// <summary>
    /// دریافت موجودیت غیر طلبه با شناسه
    /// </summary>
    /// <param name="id">شناسه موجودیت غیر طلبه</param>
    /// <returns></returns>
    [HttpGet("{id:min(1)}"), CsisAuthorize]
    public async Task<ActionResult<Result<NonStudentDto>>> GetById([FromRoute] int id) {
        return OkResult(await Mediator.Send(new GetNonStudentByIdQuery(id)));
    }

    /// <summary>
    /// ایجاد موجودیت غیر طلبه جدید
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize]
    public async Task<ActionResult<Result<long>>> Create([FromBody] CreateNonStudentCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, Result<long>.Success(result));
    }

    /// <summary>
    /// ویرایش موجودیت غیر طلبه
    /// </summary>
    /// <param name="id">شناسه موجودیت غیر طلبه</param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNonStudentCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف موجودیت غیر طلبه
    /// </summary>
    /// <param name="id">شناسه موجودیت غیر طلبه</param>
    /// <returns></returns>
    [HttpDelete("{id:min(1)}"), CsisAuthorize]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        await Mediator.Send(new DeleteNonStudentCommand(id));
        return NoContent();
    }
}
