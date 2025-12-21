using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.TargetedScores.Dtos;
using Csis.Admission.Application.Features.TargetedScores.Queries;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>امتیاز هدفمندی</summary>
[Route("/api/private/targeted-scores")]
public sealed class StudentTargetedScoresController : ApiControllerBase
{
    /// <summary>دریافت امتیاز هدفمندی</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentTargetedScoreView)]
    public async Task<ActionResult<Result<List<TargetedScoreDto>>>> GetTargetedScores([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetTargetedScoresInfoByCodmQuery(codm)));
    }

    /// <summary>دریافت امتیاز هدفمندی معیشتی</summary>
    [HttpGet("subsistence"), CsisAuthorize(PermissionsEnum.SubsistenceTargetedScoreView)]
    public async Task<ActionResult<Result<List<TargetedScoreDto>>>> GetSubsistenceTargetedScores([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetSubsistenceTargetedScoresInfoByCodmQuery(codm)));
    }

    /// <summary>لیست تغییرات سابقه هدفمندی</summary>
    [HttpGet("history-changes"), CsisAuthorize(PermissionsEnum.TargetedScoreHistoryChangesView)]
    public async Task<ActionResult<Result<List<TargetedScoreDto>>>> GetTargetedScoreHistoryChanges([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetTargetingHistoryChangesByCodmQuery(codm)));
    }
}
