using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.NonIranianStudent.Commands;
using Csis.Admission.Application.Features.Students.NonIranian.Commands;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.NonIranian;

[Route("api/private/nonIranian"), Tags("NonIranian")]
public class NonIranianController : ApiControllerBase
{
    /// <summary>ثبت فوت طلبه غیر ایرانی</summary>
    [HttpPost("death"), CsisAuthorize(permissions: PermissionsEnum.RegisterNonIranianStudentDeath)]
    public async Task<IActionResult> CreateNonIranianStudentDeath([FromBody] CreateStudentDeathCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// ثبت نسبت خانوادگی برای فرد غیر ایرانی - همچنین ثبت ازدواج طلبه غیر ایرانی با همسر غیر ایرانی نیز از این طریق انجام می‌شود
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assign-dependent"), CsisAuthorize(permissions: PermissionsEnum.AssignRelationToNonIranian)]
    public async Task<ActionResult> AssignRelationToNonIranian(AssignRelationToNonIranianCommand command) {

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق تکفل برای فرد غیر ایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent-divorce"), CsisAuthorize(permissions: PermissionsEnum.MarkDependentAsDivorced)]
    public async Task<ActionResult> MarkDependentAsDivorced(UpdateNonIranianDependentDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق همسر برای فرد غیر ایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("wife-divorce"), CsisAuthorize(permissions: PermissionsEnum.MarkWifeAsDivorced)]
    public async Task<ActionResult> MarkWifeAsDivorced(UpdateNonIranianWifeDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت ازدواج تکفل برای فرد غیر ایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent-marriage"), CsisAuthorize(permissions: PermissionsEnum.MarkDependentAsMarried)]
    public async Task<ActionResult> MarkDependentAsMarried(UpdateNonIranianDependentMarriageRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

}
