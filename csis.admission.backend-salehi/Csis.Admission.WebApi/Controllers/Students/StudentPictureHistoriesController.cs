using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.PictureHistories.Dtos;
using Csis.Admission.Application.Features.PictureHistories.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>سابقه تصاویر پرسنلی</summary>
[Route("/api/private/picture-histories")]
public sealed class StudentPictureHistoriesController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentPictureHistoryView)]
    public async Task<ActionResult<Result<List<PictureHistoryDto[]>>>> GetAuditLogs([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetPictureHistoriesByCodmQuery(codm)));
    }
}
