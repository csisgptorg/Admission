using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Features.CaseFilings.Queries;
using Csis.Admission.Application.Features.CaseFilings.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// مدیریت تشکیل پرونده
/// </summary>

[Route("api/public/case-filing"), Tags("CaseFiling")]
public sealed class CaseFilingPublicController : ApiControllerBase
{
    /// <summary>
    /// ساخت کپچا
    /// </summary>
    /// <returns></returns>
    [HttpGet("captcha")]
    public async Task<ActionResult<CaptchaDto>> GenerateCaptcha() {
        return OkResult(await Mediator.Send(new GenerateCaptchaCommand()));
    }

    /// <summary>
    /// ساخت توکن گام اول
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("create-admission-case")]
    public async Task<IActionResult> CreateAdmissionCase([FromBody] CreateAdmissionCaseFirstStepCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>
    /// تایید موبایل گام دوم
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-mobile")]
    public async Task<IActionResult> ConfirmMobile([FromBody] CreateAdmissionCaseSecondStepCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>
    /// اعتبارسنجی اطلاعات گام سوم
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("validate-case-registration")]
    public async Task<IActionResult> ValidateCaseRegistration([FromBody] CreateAdmissionCaseThirdStepCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// اعتبارسنجی اطلاعات هویتی گام چهارم
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("identity")]
    public async Task<IActionResult> Identity([FromBody] ValidateIdentityCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>
    ///  (تایید اطلاعات هویتی گام چهارم(تاییدیه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-verifying-identity-information")]
    public async Task<IActionResult> ConfirmVerifyingIdentityInformation(
        [FromBody] ConfirmIdentityInformationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// دریافت آدرس بر اساس کد پستی گام پنجم
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("address-by-postal-code")]
    public async Task<ActionResult<AddressModel>>
        GetAddressByPostalCode([FromQuery] GetAddressByPostalCodeQuery query) {
        return OkResult(await Mediator.Send(query));
    }

    [HttpPost("address-by-postal-code")]
    public async Task<IActionResult> AddressByPostalCode([FromBody] ConfirmAddressByPostalCodeCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// آپلود تصویر پروفایل گام ششم
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("profile-picture")]
    public async Task<IActionResult>
        UploadProfilePicture([FromBody] ConfirmStudentProfilePictureRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// تایید اطلاعات حساب بانکی گام هفتم
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-bank-account-information")]
    public async Task<IActionResult> ConfirmBankAccountInformation(
        [FromBody] CreateBankAccountInformationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// تایید اطلاعات شغلی گام هشتم
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-employment")]
    public async Task<IActionResult> ConfirmEmployment([FromBody] ConfirmEmploymentCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// وضعیت تشکیل پرونده دانشجو - اطلاعات کلی
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("student-case-info")]
    public async Task<ActionResult<AdmissionCaseUserDto>> GetStudentCase([FromQuery] GetStudentCaseQuery query) {
        return OkResult(await Mediator.Send(query));
    }

    /// <summary>
    /// وضعیت تشکیل پرونده دانشجو - مرحله فعلی
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("student-case-step-status")]
    public async Task<ActionResult<AdmissionCaseStatusDto>> GetStudentCaseStatus(
        [FromQuery] GetStudentCaseStatusQuery query) {
        return OkResult(await Mediator.Send(query));
    }

    /// <summary>
    /// تایید otp اخرین مرحله ثبت شده توسط کاربر
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-student-case-step-status")]
    public async Task<IActionResult> ConfirmStudentCaseStatus([FromBody] ConfirmStudentCaseStatusQuery command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// تایید اطلاعات تشکیل پرونده و ثبت درخواست
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("confirm-case-filling-request")]
    public async Task<IActionResult> ConfirmCaseFillingRequest(
        [FromBody] CompleteInformationCaseFilingCommandRequest command) {
        ;
        return OkResult(await Mediator.Send(command));
    }

    //CreateAdmissionCaseStep10CreateUserCommand
    [HttpPost("create-admission-user")]
    public async Task<IActionResult> CreateAdmissionCaseStep10(
        [FromBody] CreateAdmissionCaseStepCreateUserCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>
    /// حذف تمام درخواست های تست
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete-all-request-test")]
    public async Task<IActionResult> DeleteAllRequestTest() {
        await Mediator.Send(new DeleteAllRequestTestCommand());
        return NoContent();
    }
}
