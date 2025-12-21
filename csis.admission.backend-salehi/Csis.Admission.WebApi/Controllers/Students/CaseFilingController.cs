using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.CaseFilings.Commands;
using Csis.Admission.Application.Features.CaseFilings.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>مدیریت تشکیل پرونده</summary>

[Route("api/private/case-filing")]
public sealed class CaseFilingController : ApiControllerBase
{
    /// <summary>اعتبارسنجی اطلاعات هویتی گام چهارم</summary>
    [HttpPost("identity"),CsisAuthorize]
    public async Task<IActionResult> Identity([FromQuery] bool confirmed,[FromBody] CreateAdmissionCaseIdentityByEmployeeCommand command) {
        command.Confirmed=confirmed;
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>اعتبارسنجی اطلاعات هویتی گام چهارم</summary>
    [HttpPost("commission-identity"), CsisAuthorize]
    public async Task<IActionResult> CommissionIdentity([FromQuery] bool confirmed, [FromBody] CreateAdmissionCaseCommissionIdentityByEmployeeCommand command) {
        command.Confirmed = confirmed;
        return OkResult(await Mediator.Send(command));
    }












    /// <summary>دریافت آدرس بر اساس کد پستی گام پنجم</summary>
    [HttpGet("address-by-postal-code"), CsisAuthorize]
    public async Task<ActionResult<AddressModel>>
        GetAddressByPostalCode([FromQuery] GetAddressByPostalCodeQuery query) {
        return OkResult(await Mediator.Send(query));
    }

    [HttpPost("address-by-postal-code"), CsisAuthorize]
    public async Task<IActionResult> AddressByPostalCode([FromBody] ConfirmAddressByPostalCodeCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>آپلود تصویر پروفایل گام ششم</summary>
    [HttpPost("profile-picture"), CsisAuthorize]
    public async Task<IActionResult>
        UploadProfilePicture([FromBody] ConfirmStudentProfilePictureRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>تایید اطلاعات حساب بانکی گام هفتم</summary>
    [HttpPost("confirm-bank-account-information")]
    public async Task<IActionResult> ConfirmBankAccountInformation(
        [FromBody] CreateBankAccountInformationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>تایید اطلاعات شغلی گام هشتم</summary>
    [HttpPost("confirm-employment"), CsisAuthorize]
    public async Task<IActionResult> ConfirmEmployment([FromBody] ConfirmEmploymentCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// تایید اطلاعات تشکیل پرونده و ثبت درخواست
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-case-filling-request")]
    public async Task<IActionResult> ConfirmCaseFillingRequest([FromBody] CompleteInformationCaseFilingCommandRequest command) {
        return OkResult(await Mediator.Send(command));
    }

    [HttpPost("create-admission-user")]
    public async Task<IActionResult> CreateAdmissionCaseStep10([FromBody] CreateAdmissionCaseStepCreateUserCommand command) {
        return OkResult(await Mediator.Send(command));
    }
}
