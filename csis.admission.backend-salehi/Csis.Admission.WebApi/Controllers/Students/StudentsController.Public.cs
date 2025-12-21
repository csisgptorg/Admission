using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Authorization.Services;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Addresses.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>Student</summary>
[Route("/api/public/students"), CsisAuthorizeStudent, Tags("Students")]
public sealed class StudentsPublicController : ApiControllerBase
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    /// <inheritdoc/>
    public StudentsPublicController(ICsisAuthenticatedUserService authenticatedUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    /// <summary>اطلاعات مهم</summary>
    [HttpGet]
    public async Task<ActionResult<Result<StudentCaseDto>>> Get() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentCaseByCodmQuery(codm)));
    }

    /// <summary>
    /// اطلاعات شناسنامه ای
    /// </summary>
    /// <returns></returns>
    [HttpGet("Info")]
    public async Task<ActionResult<Result<StudentInfoDto>>> GetInfo() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentInfoByCodmQuery(codm)));
    }

    /// <summary>Get address</summary>
    [HttpGet("address")]
    public async Task<ActionResult<Result<AddressDto>>> GetAddress() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentAddressByCodmQuery(codm)));
    }

    /// <summary>موبایل</summary>
    [HttpGet("phone")]
    public async Task<ActionResult<Result<StudentPhoneDto>>> GetPhone() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentPhoneByCodmQuery(codm)));
    }

    /// <summary>اطلاعات شناسنامه تکفل</summary>
    [HttpGet("dependents")]
    public async Task<ActionResult<Result<StudentDependentDto>>> GetDependents() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentDependentsByStudentCodmQuery(codm)));
    }

    /// <summary>تمدید پرونده</summary>
    [HttpPut("extension-case")]
    public async Task<ActionResult> ExtensionCase() {
        await Mediator.Send(new StudentExtensionCaseCommand());
        return NoContent();
    }

    /// <summary>تصویر پروفایل</summary>
    [HttpGet("profile-image")]
    public async Task<ActionResult<Result<string>>> GetProfileImage() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new GetStudentProfileImageByCodmQuery(codm)));
    }

    /// <summary>بروزرسانی تصویر پروفایل</summary>
    [HttpPut("profile-picture")]
    public async Task<ActionResult> UpdateProfileImage(IFormFile file) {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        await Mediator.Send(new UpdateStudentProfilePictureRequestCommand(codm, file, null));
        return NoContent();
    }

    /// <summary>تعداد رکوردهای طلبه در بخش های مختلف</summary>
    [HttpGet("record-count")]
    public async Task<ActionResult<Result<StudentRecordCountDto>>> RecordCountByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new StudentRecordCountByCodmQuery(codm)));
    }

    /// <summary>اطلاعات طلبه که نیاز به بروزرسانی دارند</summary>
    [HttpGet("info-need-update")]
    public async Task<ActionResult<Result<StudentInfoNeedUpdateDto>>> InfoNeedUpdateByCodm() {
        var codm = int.Parse(await _authenticatedUserService.GetStudentCodmAsync());
        return OkResult(await Mediator.Send(new StudentInfoNeedUpdateByCodmQuery(codm)));
    }

    /// <summary>دریافت فرایندهای نیازمند بروز رسانی طلبه</summary>
    [HttpGet("update-wizard-steps")]
    public async Task<ActionResult<Result<StudentUpdateWizardStep[]>>> GetStudentUpdateWizardSteps() {
        return OkResult(await Mediator.Send(new GetStudentUpdateWizardStepsQuery(Codm: null)));
    }
}
