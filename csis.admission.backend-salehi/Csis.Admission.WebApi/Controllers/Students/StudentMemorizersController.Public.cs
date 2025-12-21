using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Memorizers.Dtos;
using Csis.Admission.Application.Features.Memorizers.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// حافظین
/// </summary>
[Route("/api/public/memorizer"), Tags("StudentMemorizers"), CsisAuthorizeStudent]
public sealed class MemorizersPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public MemorizersPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>طلبه</summary>
    [HttpGet]
    public async Task<ActionResult<Result<List<StudentMemorizerDto>>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentMemorizerByCodmQuery(codm)));
    }

    /// <summary>تکفل</summary>
    [HttpGet("dependents")]
    public async Task<ActionResult<Result<List<StudentMemorizerDto>>>> GetDependents() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetDependentMemorizerByCodmQuery(codm)));
    }
}
