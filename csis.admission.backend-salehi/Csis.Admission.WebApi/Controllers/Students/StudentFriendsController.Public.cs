using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.StudentFriends.Dtos;
using Csis.Admission.Application.Features.StudentFriends.Queries;
using Csis.Admission.Application.Features.StudentFriends.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <inheritdoc/>
[Route("/api/public/student-friends"), Tags("StudentFriends"), CsisAuthorizeStudent]
public sealed class StudentFriendsPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentFriendsPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>get student for friend</summary>
    [HttpPost("student")]
    public async Task<ActionResult<Result<StudentForFriendDto>>> GetStudent([FromBody] GetStudentForFriendQuery query) {
        return OkResult(await Mediator.Send(query));
    }

    /// <inheritdoc/>
    [HttpGet]
    public async Task<ActionResult<Result<List<StudentFriendDto>>>> GetsByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentFriendByCodmQuery(codm)));
    }

    /// <inheritdoc/>
    [HttpPost]
    public async Task<ActionResult<Result<int>>> Create([FromBody] CreateStudentFriendRequestCommand command) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        var result = await Mediator.Send(command with { Codm = codm });
        return OkResult(result);
    }
}
