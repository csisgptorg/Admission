using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Features.BankAccounts.Commands;
using Csis.Admission.Application.Features.BankAccounts.Queries;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>حساب بانکی</summary>
[Route("/api/public/students/bank-account-numbers"), Tags("StudentBankAccounts"), CsisAuthorizeStudent]
public sealed class StudentBankAccountsPublicController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpGet]
    public async Task<IActionResult> GetBankAccount() {
        return OkResult(await Mediator.Send(new GetFamilyBankAccountsByCodmQuery(0)));
    }

    /// <summary>
    /// ثبت درخواست برای شماره حساب سیبا طلبه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateStudentRequestBankAccountNumber([FromBody] UpdateStudentBankAccountRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت درخواست برای شماره حساب سیبا تکفل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("dependent")]
    public async Task<IActionResult> CreateDependentRequestBankAccountNumber([FromBody] UpdateDependentBankAccountRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
