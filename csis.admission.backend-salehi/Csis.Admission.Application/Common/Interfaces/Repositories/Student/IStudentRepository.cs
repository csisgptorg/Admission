using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.CommissionInfos.Dtos;
using Csis.Admission.Application.Features.CommissionsInfos.Dtos;
using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using Csis.Admission.Application.Features.PictureHistories.Dtos;
using Csis.Admission.Application.Features.Protests.Dtos;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Application.Features.TargetedScores.Dtos;
using Csis.Admission.Application.Features.ViewLogs.Dtos;

namespace Csis.Admission.Application.Common.Interfaces.Repositories.Student;

/// <summary>
/// Student repository
/// </summary>
public interface IStudentRepository
{
    /// <summary>
    /// Get student by codm
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    Task<StudentDto> GetByCodm(int codm);

    /// <summary>
    /// GetStudentByCodm
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    Task<StudentInfoDto> GetStudentInfoByCodm(int codm);

    /// <summary>اطلاعات پرونده ای طلبه</summary>
    Task<StudentCaseDto> GetCaseByCodm(int codm);

    /// <summary></summary>
    Task<StudentProfileImage> GetProfileImageByCodm(int codm);

    /// <summary>
    /// Get student address by codm
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    Task<StudentAddressDto> GetAddressByCodm(int codm);

    /// <summary>
    /// Get student phone by codm
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    Task<StudentPhoneDto> GetPhoneByCodm(int codm);

    /// <summary>دریافت تکفل</summary>
    Task<StudentDependent[]> GetDependentsByStudentCodm(int codm);

    /// <summary>کمسیون طلبه</summary>
    Task<StudentCommissionInfoDto[]> GetStudentCommissionRequestByCodm(int codm);

    /// <summary>کمسیون تکفل</summary>
    Task<DependentCommissionInfoDto[]> GetDependentCommissionRequestByCodm(int codm);

    /// <summary>امتیاز هدفمندی</summary>
    Task<TargetedScoreDto[]> GetTargetedScoresByCodm(int codm);

    /// <summary>امتیاز هدفمندی معیشتی</summary>
    Task<TargetedScoreDto[]> GetSubsistenceTargetedScoresByCodm(int codm);

    /// <summary>سابقه تصاویر پرسنلی</summary>
    Task<PictureHistoryDto[]> GetPictureHistoriesByCodm(int codm);

    /// <summary>تمدید پرونده طلبه</summary>
    Task<ProcedureResultDto> ExtensionCaseCommand(StudentExtensionCaseCommandPrc command);
    /// <summary>تمدید پرونده طلبه عادی</summary>
    Task<ProcedureResultDto> EditCaseCommand(StudentNormalEditCaseCommandPrc command);
    /// <summary>تمدید پرونده خودکار</summary>
    Task<ProcedureResultDto> ExtensionCaseCommand(ManualStudentExtensionCaseCommandPrc command);

    /// <summary>بروزرسانی تصویر پروفایل </summary>
    Task<ProcedureResultDto> UpdateProfilePictureCommand(UpdateStudentProfilePicturePrc command);

    /// <summary></summary>
    Task<ProcedureResultDto> SaveTemporaryProfilePicture(Guid fileId, byte[] picture);

    /// <summary></summary>
    Task<byte[]> GetTempProfilePicture(Guid fileId);

    /// <summary></summary>
    Task<bool> IsDualStudentApprovalRequiredForAddress(StudentAddressApprovalRequestPrc query);

    /// <summary></summary>
    Task<GenerateOtpCodeProcedureResultDto> GenerateOtpCode(int codm, long? dependentId = null);

    /// <summary></summary>
    Task PrepareTestData();

    /// <summary>استعلام از مرکز حوزوی</summary>
    Task<ValidateStudentStatusForRegisterationResultDto> ValidateStudentStatusForRegistration(ValidateStudentStatusForRegistrationCommandPrc command);

    /// <summary>تشکیل پرونده</summary>
    Task<StudentRegistrationResultDto> CreateStudentRegistrationAsync(StudentRegistrationCommandPrc command);

    /// <summary>تامین اجتماعی</summary>
    Task<StudentTaminInsuranceResultDto> GetTaminInsuranceByCodm(int codm);

    /// <summary>
    /// استعلام کدهای مرکز در نقش معرف در نقش آفرینی
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    Task<ValidateStudentStatusForRegisterationResultDto> ValidateReligiousRoleQuestionByCodm(int codm);

    /// <summary>نمایش محتوای تب مشخصات مستمری</summary>
    Task<StudentPensionStatusDto> GetContinuousInformationTabByCodm(int codm);

    /// <summary>ثبت طلاق سرپرست برای طلاب خواهر</summary>
    Task<ProcedureResultDto> UpdateStudentSisterDivorceAsync(SetStudentSisterDivorceModel command);

    /// <summary>تاریخچه آخرین مشاهدات کاربر</summary>
    Task<EmployeeLastViewStudentLogDto[]> GetEmployeeLastViewStudentLogByPersonnelId(int personnelId);

    /// <summary>تعداد رکوردهای طلبه در بخش های مختلف</summary>
    Task<StudentRecordCountDto> GetStudentRecordCount(int codm);

    /// <summary>سابقه مسکن</summary>
    /// <param name="codm">کد مرکز خدمات</param>
    Task<StudentHouseHistoryDto> GetHouseHistory(int codm);

    /// <summary>دریافت اطلاعات شهریه</summary>
    Task<StudentShahriehInfoDto> GetShahriehInfo(int codm);

    /// <summary>لیست اعتراضات طلبه</summary>
    Task<ProtestDto[]> GetProtests(int codm);

    /// <summary>اطلاعات طلبه که نیاز به بروزرسانی دارند</summary>
    Task<StudentInfoNeedUpdateDto> GetStudentInfoNeedUpdateByCodm(int codm);

    //TODO سرویس موقت هست و باید حذف شود
    /// <summary>دریافت کاردکس - اطلاعات شهریه طلبه</summary>
    //Task<List<StudentCardexShahriehDto>> GetStudentCardexShahrieh(GetStudentCardexShahriehQuery request);

    /// <summary> دریافت همسر طلبه بر اساس کد مرکز </summary>
    Task<StudentSpouseDto[]> GetSpousesByStudentCodm(int codm);

    /// <summary>
    /// دریافت اطلاعات تراز و معیشتی طلبه
    /// </summary>
    Task<StudentTotalReportDto> GetStudentTarazAndLivelihoodTotalScore(int codm);

    /// <summary>بروز رسانی شعبه و نمایندگی</summary>
    Task UpdateBranchAndAgency(UpdateBranchAndAgencyRepoCommand command);

    /// <summary>گام هایی قابل نمایش در ویزارد بیروز رسانی اطلاعات طلبه</summary>
    Task<GetUpdateWizardStepsVisibiltyDto> GetUpdateWizardStepsVisibilty(int codm);

    /// <summary>اطلاعات شناسنامه ای طلبه در کمیسیون</summary>
    Task<CommissionStudentIdentityDto> GetCommissionForNewStudent(int commissionRequestId);

    /// <summary>بروزرسانی وضعیت درخواست کمیسیون</summary>
    Task SetCommissionStatus(SetCommissionRequestRepoCommand command);

    /// <summary>دریافت وضعیت تحصیلی طلبه</summary>
    Task<GetStudentEducationStatusDto> GetStudentEducationStatus(StudentEducationStatusRepoQuery query);

    /// <summary>بروز رسانی تابعیت طلبه</summary>
    Task UpdateNonIranianStudentCitizenship(UpdateNonIranianStudentCitizenshipRepoCommand command);

    /// <summary>بروز رسانی اطلاعات شناسنامه ای تکفل</summary>
    Task UpdateNonIranianDependentCitizenship(UpdateNonIranianDependentCitizenshipRepoCommand command);

    /// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه</summary>
    Task UpdateStudentBirthCertInfo(UpdateStudentBirthCertInfoRepoCommand command);

    /// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه</summary>
    Task UpdateDependentBirthCertInfo(UpdateDependentBirthCertInfoRepoCommand command);

    //SetStudentBlocked
    /// <summary>مسدود کردن خدمات طلبه</summary>
    Task<ProcedureResultDto> SetStudentBlocked(SetStudentBlockedRepoCommand command);
    /// <summary>رفع مسدودی خدمات طلبه</summary>
    Task<ProcedureResultDto> SetStudentUnblocked(SetStudentUnBlockedRepoCommand command);
    /// <summary>ثبت فوت طلبه غیرایرانی</summary>
    Task<ProcedureResultDto> CreateStudentDeath(SetNonIranianStudentDeathPrc command);

    ///// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه غیر ایرانی</summary>
    //Task UpdateNonIranianStudentBirthCertInfo(UpdateNonIranianStudentBirthCertRepoCommand command);

    ///// <summary>بروز رسانی اطلاعات شناسنامه ای تکفلغیر ایرانی</summary>
    //Task UpdateNonIranianDependentBirthCertInfo(UpdateNonIranianDependentBirthCertRepoCommand command);
    /// <summary>تغییر شرح پرونده تکفل</summary>
    Task<ProcedureResultDto> SetDependentCaseDescription(CreateStudentDependentCaseDescriptionPrc command);
    /// <summary>بروزرسانی وضعیت فعال بودن تکفل در پرونده پذیرش</summary>
    Task<ProcedureResultDto> UpdateDependentCaseActiveStatus(UpdateStudentDependentCaseActiveStatusPrc command);
    /// <summary>بروزرسانی وضعیت غیر فعال بودن تکفل در پرونده پذیرش</summary>
    Task<ProcedureResultDto> UpdateDependentCaseDeActiveStatus(UpdateStudentDependentCaseDeActiveStatusPrc command);
    /// <summary>دریافت اطلاعات مورد نیاز برای اجرای حقوق و دستمزد</summary>
    Task<StudentDataForPayRunResult> GetDataForPayRunByCodm(int codm);
    /// <summary>دریافت اطلاعات مورد نیاز برای اجرای حقوق و دستمزد بر اساس لیست کد مرکز</summary>
    Task<List<StudentDataForPayRunResult>> GetDataForPayRunByCodmList(string codmList);

    /// <summary>دریافت اطلاعات مورد نیاز برای اجرای حقوق و دستمزد بر اساس بازه کد مرکز</summary>
    Task<List<StudentDataForPayRunResult>> GetDataForPayRunByStartEndCodm(int startCodm, int endCodm);

    /// <summary>سینک دیتای ثبت احوال طلبه</summary>
    Task<ProcedureResultDto> SetStudentWithSabteAhvalData(SetStudentWithSabteAhvalDataRepoCommand command);

    /// <summary>سینک دیتای ثبت احوال تکفل</summary>
    Task<ProcedureResultDto> SetDependentWithSabteAhvalData(SetDependentWithSabteAhvalDataRepoCommand command);

    /// <summary>سینک دیتای المصطفی طلبه</summary>
    Task<ProcedureResultDto> SetStudentWithAlmostafaData(SetStudentWithAlmostafaDataRepoCommand command);

    /// <summary>سینک دیتای المصطفی تکفل</summary>
    Task<ProcedureResultDto> SetDependentWithAlmostafaData(SetDependentWithAlmostafaDataRepoCommand command);
}
