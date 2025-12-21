using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.StudentFriends.Dtos;
using Csis.Admission.Application.Features.StudentFriends.Queries;
using Csis.Admission.Application.Features.StudentFriends.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <inheritdoc/>
[Route("/api/private/student-friends"), Tags("StudentFriends")]
public sealed class StudentFriendsController : ApiControllerBase
{
    /// <summary>
    /// گرفتن دوستان
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("student"), CsisAuthorize(PermissionsEnum.StudentFriendView)]
    public async Task<ActionResult<Result<StudentForFriendDto>>> GetStudentForFriend([FromBody] GetStudentForFriendQuery query) {
        return OkResult(await Mediator.Send(query));
    }

    /// <summary>
    /// گرفتن دوستان بر اساس کد مرکز
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentFriendView)]
    public async Task<ActionResult<Result<List<StudentFriendDto>>>> GetsByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentFriendByCodmQuery(codm)));
    }

    /// <summary>
    /// ثبت دوست
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentFriendRegister)]
    public async Task<ActionResult<Result<long>>> Create([FromBody] CreateStudentFriendRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// حذف دوست
    /// </summary>
    /// <param name="codm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, CsisAuthorize(PermissionsEnum.StudentFriendDelete)]
    public async Task<ActionResult<Result>> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteStudentFriendRequestCommand(codm, id));
        return NoContent();
    }
}
