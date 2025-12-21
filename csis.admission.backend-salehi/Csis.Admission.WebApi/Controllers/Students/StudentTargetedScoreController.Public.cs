using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.TargetedScores.Dtos;
using Csis.Admission.Application.Features.TargetedScores.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>امتیاز هدفمندی</summary>
[Route("/api/public/studnets/targeted-scores"), CsisAuthorizeStudent, Tags("StudentTargetedScores")]
public sealed class StudentTargetedScorePublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentTargetedScorePublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>دریافت امتیاز هدفمندی</summary>
    [HttpGet]
    public async Task<ActionResult<Result<List<TargetedScoreDto>>>> GetTargetedScores() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetTargetedScoresInfoByCodmQuery(codm)));
    }

    /// <summary>دریافت امتیاز هدفمندی معیشتی</summary>
    [HttpGet("subsistence")]
    public async Task<ActionResult<Result<List<TargetedScoreDto>>>> GetSubsistenceTargetedScores() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetSubsistenceTargetedScoresInfoByCodmQuery(codm)));
    }

    /// <summary>لیست تغییرات سابقه هدفمندی</summary>
    [HttpGet("history-changes")]
    public async Task<ActionResult<Result<List<TargetedScoreDto>>>> GetTargetedScoreHistoryChanges() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetTargetingHistoryChangesByCodmQuery(codm)));
    }
}
