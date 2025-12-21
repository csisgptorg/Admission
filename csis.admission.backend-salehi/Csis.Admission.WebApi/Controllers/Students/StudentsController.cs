using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Addresses.Dtos;
using Csis.Admission.Application.Features.BlockedServices.Commands;
using Csis.Admission.Application.Features.Students.Commands;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Csis.Authorization.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>
/// Student
/// </summary>
[Route("/api/private/students")]
public sealed class StudentsController(
    ICsisWsmService wsmService,
    ICsisAuthenticatedUserService authenticatedUserService)
    : ApiControllerBase
{

    /// <summary>اطلاعات مهم</summary>
    [HttpGet("{codm:min(1)}"), CsisAuthorize(PermissionsEnum.StudentView, PermissionsEnum.ImamJamaatView)]
    public async Task<ActionResult<Result<StudentDto>>> GetByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentByCodmQuery(codm)));
    }

    /// <summary>
    /// Get profile image
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("{codm:min(1)}/profile-image"), CsisAuthorize(PermissionsEnum.StudentProfileImageView)]
    public async Task<ActionResult<Result<string>>> GetProfileImageByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentProfileImageByCodmQuery(codm)));
    }

    /// <summary>
    /// Get address
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("{codm:min(1)}/address"), CsisAuthorize(PermissionsEnum.StudentAddressView)]
    public async Task<ActionResult<Result<AddressDto>>> GetAddressByCodm([FromRoute] int codm) {
        var result = await Mediator.Send(new GetStudentAddressByCodmQuery(codm));
        return OkResult(result);
    }

    /// <summary>
    /// Get phone
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("{codm:min(1)}/phone"), CsisAuthorize(PermissionsEnum.StudentPhoneView)]
    public async Task<ActionResult<Result<StudentPhoneDto>>> GetPhoneByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentPhoneByCodmQuery(codm)));
    }

    /// <summary>
    /// Get phone
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("{codm:min(1)}/dependents"), CsisAuthorize(PermissionsEnum.DependentPhoneView)]
    public async Task<ActionResult<Result<StudentDependentDto>>> GetDependentsByStudentCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentDependentsByStudentCodmQuery(codm)));
    }

    /// <summary>تعداد رکوردهای طلبه در بخش های مختلف</summary>
    [HttpGet("record-count"), CsisAuthorize(PermissionsEnum.StudentRecordCount)]
    public async Task<ActionResult<Result<StudentRecordCountDto>>> RecordCountByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new StudentRecordCountByCodmQuery(codm)));
    }

    /// <summary>جستجو پیشرفته طلبه</summary>
    [HttpPost("advanced-search"), CsisAuthorize(PermissionsEnum.StudentAdvancedSearch)]
    [DynamicSearch<StudentSummary>]
    public async Task<ActionResult<Result<List<StudentAdvancedSearchDto>>>> StudentAdvancedSearch([FromBody] StudentAdvancedSearchQuery query) {
        return PaginatedResult(await Mediator.Send(query));
    }

    /// <summary>جستجو پیشرفته تکفل</summary>
    [HttpPost("dependents/advanced-search"), CsisAuthorize(PermissionsEnum.DependentAdvancedSearch)]
    [DynamicSearch<StudentSummary>]
    public async Task<ActionResult<Result<List<DependentAdvancedSearchDto>>>> DependentAdvancedSearch([FromBody] DependentAdvancedSearchQuery query) {
        return PaginatedResult(await Mediator.Send(query));
    }

    /// <summary>اطلاعات شهریه طلبه</summary>
    [HttpGet("shahrieh-info"), CsisAuthorize(PermissionsEnum.StudentShahriehInfoView)]
    public async Task<ActionResult<Result<StudentShahriehInfoDto>>> GetStudentShahriehInfo([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentShahriehInfoByCodmQuery(codm)));
    }

    /// <summary>دریافت اطلاعات طلبه در مراکز حوزوی</summary>
    [HttpPost("info-from-marakez-howzavi"), CsisAuthorize(PermissionsEnum.StudentInfoInMarakezHowzaviView)]
    public async Task<ActionResult<Result<object>>> Test(StudentInfoInMarakezHowzaviRequest query) {

        if ( string.IsNullOrWhiteSpace(query.Codm) ) {
            query.Codm = "0";
        }

        if ( string.IsNullOrWhiteSpace(query.ApprovalCenter) ) {
            query.ApprovalCenter = "0";
        }

        if ( string.IsNullOrWhiteSpace(query.CaseNumberInApprovalCenter) ) {
            query.CaseNumberInApprovalCenter = "0";
        }

        if ( string.IsNullOrWhiteSpace(query.NationalCode) ) {
            query.NationalCode = "0";
        }

        if ( string.IsNullOrWhiteSpace(query.YektaCode) ) {
            query.YektaCode = "0";
        }

        if ( string.IsNullOrWhiteSpace(query.DataGroup) ) {
            query.DataGroup = "0";
        }

        var json= await wsmService.GetStudentInfoInMarakezHowzavi(query, CancellationToken.None);
        var result = JsonSerializer.Deserialize<dynamic>(json);
        return Ok(result);
    }

    /// <summary>تمدید پرونده خودکار</summary>
    [HttpPut("extension-case/{codm:min(1)}"), CsisAuthorize(PermissionsEnum.StudentExtensionCase)]
    public async Task<ActionResult> AutoExtensionCase([FromRoute] int codm) {
        await Mediator.Send(new StudentExtensionCaseCommand(codm));
        return NoContent();
    }

    /// <summary>تمدید پرونده دستی</summary>
    [HttpPut("manual-extension-case"), CsisAuthorize(PermissionsEnum.StudentExtensionCase)]
    public async Task<ActionResult> ExtensionCase([FromBody] ManualStudentExtensionCaseRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// calculate case extension
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("calculate-extension-case/{codm:min(1)}"), CsisAuthorize(PermissionsEnum.StudentExtensionCase)]
    public async Task<ActionResult<Result<bool>>> CalculateExtensionCase([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new CalculateExtensionCaseTimeQuery(codm)));
    }


    /// <summary>اطلاعات مهم</summary>
    [HttpGet("case/{codm:min(1)}"), CsisAuthorize(PermissionsEnum.StudentView)]
    public async Task<ActionResult<Result<StudentCaseDto>>> Get([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentSummaryCaseByCodmQuery(codm)));
    }

    /// <summary>سینک اطلاعات شناسنامه ای طلبه براساس ثبت احوال (ایرانی) یا المصطفی (غیرایرانی)</summary>
    [HttpPut("sync-birth-cert"), CsisAuthorize]
    public async Task<IActionResult> SyncStudentBirthCert([FromQuery] bool confirmed, [FromBody] SyncStudentBirthCertCommand command) {
        command.Confirmed = confirmed;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>سینک اطلاعات شناسنامه ای تکفل براساس ثبت احوال (ایرانی) یا المصطفی (غیرایرانی)</summary>
    [HttpPut("dependent/sync-birth-cert"), CsisAuthorize]
    public async Task<IActionResult> SyncDependentBirthCert([FromQuery] bool confirmed, [FromBody] SyncDependentBirthCertCommand command) {
        command.Confirmed = confirmed;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ویرایش اطلاعات شناسنامه ای طلبه</summary>
    [HttpPut("birth-cert"), CsisAuthorize]
    public async Task<IActionResult> UpdateStudentBirthCert([FromBody] UpdateStudentBirthCertCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ویرایش اطلاعات شناسنامه ای تکفل</summary>
    [HttpPut("dependent/birth-cert"), CsisAuthorize]
    public async Task<IActionResult> UpdateDependentBirthCert([FromBody] UpdateDependentBirthCertCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
    //TODO: باید از سید پرسیده شود, دوبار پیاده سازی شده است
    ///// <summary>سینک اطلاعات شناسنامه ای طلبه براساس ثبت احوال - بدون نیاز به ورودی کدملی و تاریخ تولد</summary>
    //[HttpPut("{codm:min(1)}/sync-birth-cert-by-codm"), CsisAuthorize]
    //public async Task<IActionResult> SyncStudentBirthCertByCodm([FromRoute] int codm, [FromQuery] bool confirmed) {
    //    var command = new SyncStudentBirthCertByCodmCommand { Codm = codm, Confirmed = confirmed };
    //    await Mediator.Send(command);
    //    return NoContent();
    //}

    ///// <summary>سینک اطلاعات شناسنامه ای تکفل براساس ثبت احوال - بدون نیاز به ورودی کدملی و تاریخ تولد</summary>
    //[HttpPut("dependent/{id:min(1)}/sync-birth-cert-by-id"), CsisAuthorize]
    //public async Task<IActionResult> SyncDependentBirthCertById([FromRoute] long id, [FromQuery] bool confirmed) {
    //    var command = new SyncDependentBirthCertByIdCommand { Id = id, Confirmed = confirmed };
    //    await Mediator.Send(command);
    //    return NoContent();
    //}

    /// <summary>ویرایش اطلاعات شناسنامه ای طلبه غیر ایرانی</summary>
    [HttpPut("birth-cert/non-iranian"), CsisAuthorize]
    public async Task<IActionResult> UpdateStudentBirthCert([FromBody] UpdateNonIranianStudentBirthCertCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>ویرایش اطلاعات شناسنامه ای تکفل غیر ایرانی</summary>
    [HttpPut("dependent/birth-cert/non-iranian"), CsisAuthorize]
    public async Task<IActionResult> UpdateDependentBirthCert([FromBody] UpdateNonIranianDependentBirthCertCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>تغییر تابعیت طلبه غیر ایرانی به ایرانی براساس ثبت احوال</summary>
    [HttpPut("citizenship/non-iranian"), CsisAuthorize]
    public async Task<IActionResult> UpdateNonIranianStudentCitizenship([FromBody] UpdateNonIranianStudentCitizenshipCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>تغییر تابعیت تکفل غیر ایرانی به ایرانی براساس ثبت احوال</summary>
    [HttpPut("dependent/citizenship/non-iranian"), CsisAuthorize]
    public async Task<IActionResult> UpdateNonIranianDependentCitizenship([FromBody] UpdateNonIranianDependentCitizenshipCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>بروزرسانی تصویر پروفایل</summary>
    [HttpPut("profile-picture")]
    public async Task<ActionResult> UpdateProfileImage(IFormFile file, [FromQuery] int codm) {
        var result = await Mediator.Send(new UpdateStudentProfilePictureRequestCommand(codm, file, true));
        return OkResult(result);
    }

    /// <summary>بروزرسانی تصویر پروفایل از ثبت احوال</summary>
    [HttpPost("{codm:min(1)}/profile-picture-from-civil-registry"), CsisAuthorize]
    public async Task<ActionResult<Result<long>>> UpdateProfilePictureFromCivilRegistry([FromRoute] int codm, [FromQuery] bool confirmed) {
        var result = await Mediator.Send(new UpdateStudentProfilePictureFromCivilRegistryRequestCommand(codm, confirmed));
        return OkResult(result);
    }

    //block student case
    /// <summary>مسدودسازی پرونده طلبه</summary>
    [HttpPost("block"), CsisAuthorize(permissions: PermissionsEnum.StudentBlockCase)]
    public async Task<IActionResult> BlockStudentCase([FromBody] CreateStudentCaseBlockRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>رفع مسدودسازی پرونده طلبه</summary>
    [HttpPost("unblock"), CsisAuthorize(permissions: PermissionsEnum.StudentUnblockCase)]
    public async Task<IActionResult> UnblockStudentCase([FromBody] CreateStudentCaseUnblockRequestCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    //StudentNormalEditCaseCommand
    /// <summary>ویرایش عادی پرونده</summary>
    [HttpPut("normal-edit-case"), CsisAuthorize(permissions: PermissionsEnum.StudentNormalEditCase)]
    public async Task<IActionResult> StudentNormalEditCase([FromBody] StudentNormalEditCaseRequestCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
