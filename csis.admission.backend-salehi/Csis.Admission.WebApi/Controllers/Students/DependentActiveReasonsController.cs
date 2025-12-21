using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.DependentActiveReasons.Dtos;
using Csis.Admission.Application.Features.DependentActiveReasons.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت دلیل رفع انسداد پرونده
/// </summary>
[Route("/api/private/dependent-active-reasons")]
public sealed class DependentActiveReasonsController : ApiControllerBase
{
    /// <summary>
    /// دریافت همه دلیل رفع انسداد پرونده ها بدون صفحه بندی
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Result<List<DependentActiveReasonDto>>>> GetAll() {
        return OkResult(await Mediator.Send(new GetAllDependentActiveReasonsQuery()));
    }
}
