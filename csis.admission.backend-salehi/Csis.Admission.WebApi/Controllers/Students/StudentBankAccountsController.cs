using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.BankAccounts.Queries;
using Csis.Admission.Application.Features.BankAccounts.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>حساب بانکی</summary>
[Route("/api/private/students/bank-account-numbers")]
public sealed class StudentBankAccountsController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet("{codm}"),CsisAuthorize(PermissionsEnum.StudentDependetnBankAccountView)]
    public async Task<IActionResult> GetBankAccount([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetFamilyBankAccountsByCodmQuery(codm)));
    }

    /// <summary>
    /// ثبت درخواست برای شماره حساب سیبا طلبه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost, CsisAuthorize(PermissionsEnum.StudentBankAccountRegister)]
    public async Task<IActionResult> CreateStudentRequestBankAccount([FromBody] UpdateStudentBankAccountRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت درخواست برای شماره حساب سیبا تکفل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent"), CsisAuthorize(PermissionsEnum.DependentBankAccountRegister)]
    public async Task<IActionResult> CreateDependentRequestBankAccount([FromBody] UpdateDependentBankAccountRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
