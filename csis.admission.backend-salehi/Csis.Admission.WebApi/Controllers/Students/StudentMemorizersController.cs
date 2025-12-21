using Csis.Abstractions.Results;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Memorizers.Commands;
using Csis.Admission.Application.Features.Memorizers.Dtos;
using Csis.Admission.Application.Features.Memorizers.Queries;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>حافظین</summary>
[Route("/api/private/memorizer")]
public sealed class StudentMemorizersController : ApiControllerBase
{
    /// <summary>طلبه</summary>
    [HttpGet, CsisAuthorize(PermissionsEnum.StudentMemorizerView)]
    public async Task<ActionResult<Result<List<StudentMemorizerDto>>>> Get([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentMemorizerByCodmQuery(codm)));
    }

    /// <summary>تکفل</summary>
    [HttpGet("dependents"), CsisAuthorize(PermissionsEnum.StudentMemorizerView)]
    public async Task<ActionResult<Result<List<StudentMemorizerDto>>>> GetDependents([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetDependentMemorizerByCodmQuery(codm)));
    }

    /// <summary>حذف حافظ</summary>
    [HttpDelete, CsisAuthorize(PermissionsEnum.SeniorPersonnel, PermissionsEnum.StudentMemorizerDelete)]
    public async Task<IActionResult> Delete([FromQuery] int codm, [FromQuery] int id) {
        await Mediator.Send(new DeleteMemorizerRequestCommand(codm, id));
        return NoContent();
    }

    /// <summary>
    /// ارتباط داده ای
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("student/data-import"), CsisAuthorizeApiKey(PermissionsEnum.DataImport), ApiKeyHeader]
    public async Task<ActionResult<Result<int>>> DataImport([FromBody] MemorizerDataImportCommand command) {
        return OkResult(await Mediator.Send(command));
    }
}
