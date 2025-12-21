using Csis.Admission.Application.Features.Marriages.Dtos;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Application.Features.StudentDependents.Commands;
using Csis.Admission.Application.Features.StudentDependents.Dtos;
using Csis.Admission.Application.Features.StudentDependents.Queries;
using Csis.Authorization.Services;
using Csis.Utilities.Extensions;

namespace Csis.Admission.WebApi.Controllers;

/// <inheritdoc/>
[Route("/api/public/student-dependents"), Tags("StudentDependents"), CsisAuthorizeStudent]
public sealed class StudentDependentsPublicController(ICsisAuthenticatedUserService userService) : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpPost("spouse-registry")]
    public async Task<IActionResult> SpouseRegistry([FromBody] StudentSpouseRegistryRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <inheritdoc/>
    [HttpPost("identify-spouse")]
    public async Task<ActionResult<Result<SpouseIdentifyDto>>> IdentifySpouse([FromBody] IdentifySpouseFromSabteAhvalCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <inheritdoc/>
    [HttpPost("child-registry")]
    public async Task<IActionResult> ChildRegistry([FromBody] StudentChildRegistryCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <inheritdoc/>
    [HttpGet("spouse")]
    public async Task<ActionResult<Result<List<DependentSpousesDto>>>> GetSpouseInfo() {
        var codm = (await userService.GetStudentCodmAsync()).ToInt();
        var spouse = await Mediator.Send(new GetDependentSpousesQuery(codm));
        return OkResult(spouse);
    }

    /// <inheritdoc/>
    [HttpGet("dependent")]
    public async Task<ActionResult<Result<List<FamilyInfoDto>>>> GetDependentInfo() {
        var codm = (await userService.GetStudentCodmAsync()).ToInt();
        var dependents = await Mediator.Send(new GetFamilySinglesByCodmQuery{Codm = codm});
        return OkResult(dependents);
    }
}
