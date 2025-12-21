using Csis.Admission.Application.Features.NonIranianStudent.Commands;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.NonIranian;

/// <summary>
/// کنترلر مربوط به افراد غیر ایرانی - دسترسی عمومی
/// </summary>
[Route("api/public/nonIranian"),Tags("NonIranian")]
public class NonIranianPublicController : ApiControllerBase
{
    /// <summary>
    /// ثبت نسبت خانوادگی برای فرد غیر ایرانی - همچنین ثبت ازدواج طلبه غیر ایرانی با همسر غیر ایرانی نیز از این طریق انجام می‌شود
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assign-dependent"), CsisAuthorizeStudent]
    public async Task<ActionResult> AssignRelationToNonIranian(AssignRelationToNonIranianCommand command) {

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق تکفل برای فرد غیر ایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent-divorce"), CsisAuthorizeStudent]
    public async Task<ActionResult> MarkDependentAsDivorced(UpdateNonIranianDependentDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت طلاق همسر برای فرد غیر ایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("wife-divorce"), CsisAuthorizeStudent]
    public async Task<ActionResult> MarkWifeAsDivorced(UpdateNonIranianWifeDivorceRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت ازدواج تکفل برای فرد غیر ایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent-marriage"), CsisAuthorizeStudent]
    public async Task<ActionResult> MarkDependentAsMarried(UpdateNonIranianDependentMarriageRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    
}
