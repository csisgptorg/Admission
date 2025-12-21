using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Branches.Dtos;
using Csis.Admission.Application.Features.Branches.Queries;
using Csis.Admission.Application.Features.Cities.Dtos;
using Csis.Admission.Application.Features.Cities.Queries;
using Csis.Admission.Application.Features.CompleteStudentInfos.Queries;
using Csis.Admission.Application.Features.Employments.Queries;
using Csis.Admission.Application.Features.Houses.Dtos;
using Csis.Admission.Application.Features.Houses.Queries;
using Csis.Admission.Application.Features.HousingAdmissionInfo.Commands;
using Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;
using Csis.Admission.Application.Features.HousingAdmissionInfo.Queries;
using Csis.Admission.Application.Features.Marriages.Queries;
using Csis.Admission.Application.Features.Provinces.Dtos;
using Csis.Admission.Application.Features.Provinces.Queries;
using Csis.Admission.Application.Features.StudentDependents.Dtos;
using Csis.Admission.Application.Features.StudentDependents.Queries;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Iranian.Queries;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <inheritdoc/>
[Route("/api/private/client-application-requests")]
public sealed class ClientApplicationRequestsController : ApiControllerBase
{
    /// <summary>Get profile image</summary>
    [HttpGet("students/{codm:min(1)}/profile-image"), CsisAuthorizeApiKey]
    public async Task<ActionResult<Result<string>>> GetStudentProfileImageByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentProfileImageByCodmQuery(codm)));
    }

    /// <summary>اطلاعات مهم پرونده طلبه</summary>
    [HttpGet("students/{codm:min(1)}/case"), CsisAuthorizeApiKey]
    public async Task<ActionResult<Result<StudentCaseDto>>> ApplicationGetByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentCaseByCodmQuery(codm)));
    }

    /// <summary>اطلاعات کامل طلبه</summary>
    [HttpGet("students/complete-info"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<CompleteStudentInfo>>>
        GetCompleteStudentInfoByCodmQuery([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetCompleteStudentInfoByCodmQuery(codm)));
    }

    /// <summary>دریافت تصویر پروفایل کاربر</summary>
    [HttpGet("students/profile-image"), CsisAuthorizeApiKey(PermissionsEnum.StudentProfileImageAppView)]
    public async Task<ActionResult<Result<string>>> ProfileImageByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentProfileImageByCodmQuery(codm)));
    }

    /// <summary>
    /// دریافت مواردی مثل تراز طلبه, هدفمندی طلبه, معیشت طلبه بر اساس کد مرکز
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("total-report/{codm}"), CsisAuthorizeApiKey]
    public async Task<ActionResult<Result<StudentTotalReportDto>>> GetStudentCaseByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentTotalReportByStudentCodmQuery(codm)));
    }

    /// <summary>
    /// کارت الکترونیکی دانشجو بر اساس کد مرکز
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("students/electronic-card/{codm}"), CsisAuthorizeApiKey]
    public async Task<ActionResult<Result<StudentECardDto>>> GetStudentElectronicCardByCodm([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentElectronicCardByCodmQuery(codm)));
    }

    //GetDataForPayRunByCodm
    /// <summary>
    /// دریافت اطلاعات برای اجرای پرداخت بر اساس کد مرکز (PayRun)
    /// </summary>
    [HttpGet("students/data-for-pay-run"), CsisAuthorizeApiKey(permission: PermissionsEnum.GetDataForPayRun)]
    public async Task<ActionResult<Result<StudentDataForPayRunResult>>> GetDataForPayRunByCodm([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetDataForPayRunByCodmQuery(codm)));
    }

    /// <summary>
    /// دریافت اطلاعات برای اجرای پرداخت بر اساس لیست کد مرکز (PayRun)
    /// </summary>
    [HttpPost("students/data-for-pay-run-list"), CsisAuthorizeApiKey(permission: PermissionsEnum.GetDataForPayRun)]
    public async Task<ActionResult<Result<List<StudentDataForPayRunResult>>>> GetDataForPayRunByCodmList([FromBody] GetDataForPayRunByCodmListQuery request) {
        return OkResult(await Mediator.Send(request));
    }

    /// <summary>
    /// دریافت اطلاعات برای اجرای پرداخت بر اساس بازه کد مرکز (PayRun)
    /// </summary>
    [HttpPost("students/data-for-pay-run-start-end-codm"), CsisAuthorizeApiKey(permission: PermissionsEnum.GetDataForPayRun)]
    public async Task<ActionResult<Result<List<StudentDataForPayRunResult>>>> GetDataForPayRunByStartEndCodm([FromBody] GetDataForPayRunByStartEndCodmQuery request) {
        return OkResult(await Mediator.Send(request));
    }

    /// <summary>
    /// دریافت بیشترین کد مرکز برای اجرای پرداخت (PayRun)
    /// </summary>
    /// <returns></returns>
    [HttpGet("students/max-codm-for-pay-run"), CsisAuthorizeApiKey(permission: PermissionsEnum.GetDataForPayRun)]
    public async Task<ActionResult<Result<int>>> GetMaxCodmForPayRun() {
        return OkResult(await Mediator.Send(new GetMaxCodmForPayRunQuery()));
    }

    /// <summary>دریافت لیست استانها</summary>
    [HttpGet("students/provinces"), CsisAuthorizeApiKey]
    public async Task<ActionResult<Result<List<ProvinceDto>>>> GetProvinces() {
        return OkResult(await Mediator.Send(new GetProvincesQuery()));
    }

    /// <summary>لیست</summary>
    [HttpGet("students/cities"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<CityDto[]>>> GetAll([FromQuery] short? ProvinceId) {
        return OkResult(await Mediator.Send(new GetCitiesQuery(ProvinceId)));
    }

    //ValidateBranchesByProviceIdQuery
    /// <summary>اعتبارسنجی شعب بر اساس استان</summary>
    [HttpGet("students/validate-branches-by-province-id"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<bool>>> ValidateBranchesByProviceId([FromQuery] short ProvinceId, [FromQuery] short BranchId) {
        return OkResult(await Mediator.Send(new ValidateBranchesByProviceIdQuery(BranchId, ProvinceId)));
    }

    #region Housing Admission Info
    /// <summary>
    /// دریافت اطلاعات پایه طلبه
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <returns></returns>
    [HttpGet("basic/{codm}"), CsisAuthorizeApiKey()]
    public async Task<IActionResult> GetBasicInfo([FromRoute] int codm) {
        var query = new GetHousingBasicInfoByCodmQuery(codm);
        var result = await Mediator.Send(query);
        return OkResult(result);
    }

    /// <summary>
    /// دریافت اطلاعات مسکن و اعتراضات
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <returns></returns>
    [HttpGet("status/{codm}"), CsisAuthorizeApiKey()]
    public async Task<IActionResult> GetStatusInfo([FromRoute] int codm) {
        var query = new GetHousingStatusInfoByCodmQuery(codm);
        var result = await Mediator.Send(query);
        return OkResult(result);
    }

    /// <summary>
    /// دریافت اطلاعات فعالیت‌های علمی و فرهنگی
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <returns></returns>
    [HttpGet("activities/{codm}"), CsisAuthorizeApiKey()]
    public async Task<IActionResult> GetActivitiesInfo([FromRoute] int codm) {
        var query = new GetHousingActivitiesInfoByCodmQuery(codm);
        var result = await Mediator.Send(query);
        return OkResult(result);
    }

    /// <summary>دریافت اجاره نامه های فعال</summary>
    [HttpGet("students/active-tenants"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<List<ActiveTenantDto>>>> GetActiveTenants([FromQuery] int? codm) {
        return OkResult(await Mediator.Send(new GetActiveTenantsQuery(codm)));
    }


    #endregion

    /// <summary>
    /// اطلاعات شناسنامه ای
    /// </summary>
    /// <returns></returns>
    [HttpGet("students/Info/{codm}"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<StudentInfoDto>>> GetInfo([FromRoute] int codm) {
        return OkResult(await Mediator.Send(new GetStudentInfoByCodmQuery(codm)));
    }

    /// <summary>
    /// اطلاعات افراد تحت تکفل بر اساس کد مرکز
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    [HttpGet("students/dependent/{codm}"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<List<FamilyInfoDto>>>> GetDependentInfo([FromRoute] int codm) {
        var dependents = await Mediator.Send(new GetFamilySinglesByCodmQuery { Codm = codm });
        return OkResult(dependents);
    }

    /// <summary>
    /// دریافت لیست استان بر اساس شناسه شعبه
    /// </summary>
    [HttpGet("students/provinces-by-branch-id"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<BranchDto>>> GetProvincesByBranchId([FromQuery] short branchId) {
        return OkResult(await Mediator.Send(new GetProvinceByBranchIdQuery(branchId)));
    }

    /// <summary>دریافت لیست شعب</summary>
    [HttpGet("students/branches"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<List<BranchDto>>>> GetAllBranches() {
        return OkResult(await Mediator.Send(new GetBranchesQuery(null)));
    }

    /// <summary>
    /// دریافت دهک بر اساس لیست کدمرکز
    /// </summary>
    [HttpPost("students/deciles"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<Dictionary<int, short?>>>> GetDeciles([FromBody] List<int> codmList) {
        return OkResult(await Mediator.Send(new GetDecileByCodmQuery(codmList)));
    }

    /// <summary>
    /// دریافت افراد تحت تکفل بر اساس کد مرکز
    /// </summary>
    [HttpGet("students/with-dependents"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<StudentWithDependentsDto>>> GetStudentDependents([FromQuery] int codm) {
        return OkResult(await Mediator.Send(new GetStudentDependentsQuery(codm)));
    }

    /// <summary>
    /// دریافت اطلاعات طلبه برای جهيزيه
    /// </summary>
    [HttpPost("students/dowry-info"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<InfoForDowryServiceDto>>> GetStudentInfoForDowryService([FromBody] List<int?> codms) {
        return OkResult(await Mediator.Send(new GetStudentInfoForDowryService(codms)));
    }

    [HttpPost("students/bulk-codm-by-national-code"), CsisAuthorizeApiKey()]
    public async Task<ActionResult<Result<List<GetStudentCodmByNationalCodeDto>>>> GetBulkCodmByNationalCode([FromBody] List<string> nationalCodes) {
        return OkResult(await Mediator.Send(new GetBulkCodmByNationalCodeCommand(nationalCodes)));
    }

}
