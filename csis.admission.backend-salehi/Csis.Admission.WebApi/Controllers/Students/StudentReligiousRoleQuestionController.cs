using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Features.ReligiousRoleQuestions.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>نقش آفرینی</summary>
[Route("/api/public/student/religious-rule-question"), CsisAuthorizeStudent]
public sealed class StudentReligiousRoleQuestionController : ApiControllerBase {

    /// <summary>
    /// ایجاد یا ویرایش درخواست نقش آفرینی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateRequestReligiousRoleQuestion([FromBody] CreateRequestReligiousRoleQuestionCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
