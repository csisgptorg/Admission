using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.DependentDeActiveReasons.Dtos;
using Csis.Admission.Application.Features.DependentDeActiveReasons.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت دلیل انسداد پرونده
/// </summary>
[Route("/api/private/dependent-de-active-reasons")]
public sealed class DependentDeActiveReasonsController : ApiControllerBase
{
    /// <summary>
    /// دریافت همه دلیل انسداد پرونده ها بدون صفحه بندی
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Result<List<DependentDeActiveReasonDto>>>> GetAll() {
        return OkResult(await Mediator.Send(new GetAllDependentDeActiveReasonsQuery()));
    }
}
