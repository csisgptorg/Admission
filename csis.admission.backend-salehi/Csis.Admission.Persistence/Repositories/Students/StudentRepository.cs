using AutoMapper;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.CommissionInfos.Dtos;
using Csis.Admission.Application.Features.CommissionsInfos.Dtos;
using Csis.Admission.Application.Features.PictureHistories.Dtos;
using Csis.Admission.Application.Features.Protests.Dtos;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Application.Features.TargetedScores.Dtos;
using Csis.Admission.Application.Features.ViewLogs.Dtos;
using Csis.Admission.Domain.Entities;
using System.Linq.Expressions;

namespace Csis.Admission.Persistence.Repositories.Students;

internal sealed class StudentRepository(IMapper mapper, AppDapperContext dapper) : IStudentRepository
{
    public async Task<StudentDto> GetByCodm(int codm) {
        var student = await dapper.ExecuteProcedureSingleOrDefault<Student>(ProcedureName.GetStudentInfoV4, new { codm });
        var result = mapper.Map<StudentDto>(student);
        return result;
    }

    public async Task<StudentInfoDto> GetStudentInfoByCodm(int codm) {
        var student = await dapper.ExecuteProcedureSingleOrDefault<StudentInfo>(ProcedureName.GetStudentBirthCertInfoV4, new { codm });
        var result = mapper.Map<StudentInfoDto>(student);
        return result;
    }

    /// <summary>اطلاعات پرونده ای طلبه</summary>
    public async Task<StudentCaseDto> GetCaseByCodm(int codm) {
        var student = await dapper.ExecuteProcedureSingleOrDefault<StudentCase>(ProcedureName.GetStudentCaseInfoV4, new { codm });
        var result = mapper.Map<StudentCaseDto>(student);
        return result;
    }

    public async Task<StudentProfileImage> GetProfileImageByCodm(int codm) {
        var studentProfileImage = await dapper.ExecuteProcedureSingleOrDefault<StudentProfileImage>(ProcedureName.GetStudentPictureV4, new { codm });
        if ( studentProfileImage != null ) {
            return studentProfileImage;
        }

        return null;
    }

    public async Task<StudentAddressDto> GetAddressByCodm(int codm) {
        var studentAddress = await dapper.ExecuteProcedureSingleOrDefault<StudentAddress>(ProcedureName.GetAddressV4, new { codm });
        var result = mapper.Map<StudentAddressDto>(studentAddress);
        return result;
    }

    public async Task<StudentPhoneDto> GetPhoneByCodm(int codm) {
        var studentPhone = await dapper.ExecuteProcedureSingleOrDefault<StudentPhone>(ProcedureName.GetPhoneV4, new { codm });
        var result = mapper.Map<StudentPhoneDto>(studentPhone);
        return result;
    }

    public async Task<StudentDependent[]> GetDependentsByStudentCodm(int codm) {
        var result = await dapper.ExecuteProcedureToList<StudentDependent>(ProcedureName.GetDependentInfoV4, new { codm });
        return [.. result];
    }
    public async Task<StudentSpouseDto[]> GetSpousesByStudentCodm(int codm) {
        var result = await dapper.ExecuteProcedureToList<StudentSpouseDto>(ProcedureName.GetSpouseInfoV4, new { codm });
        return [.. result];
    }

    public async Task<StudentCommissionInfoDto[]> GetStudentCommissionRequestByCodm(int codm) {
        var commissions = await dapper.ExecuteProcedureToList<CommissionInfo>(ProcedureName.GetStudentCommission, new { codm });
        var result = commissions.Select(mapper.Map<StudentCommissionInfoDto>).ToArray();
        return result;
    }

    public async Task<DependentCommissionInfoDto[]> GetDependentCommissionRequestByCodm(int codm) {
        var commissions = await dapper.ExecuteProcedureToList<CommissionInfo>(ProcedureName.GetDependentCommission, new { codm });
        var result = commissions.Select(mapper.Map<DependentCommissionInfoDto>).ToArray();
        return result;
    }

    public async Task<TargetedScoreDto[]> GetTargetedScoresByCodm(int codm) {
        var targetedScores = await dapper.ExecuteProcedureToList<TargetedScore>(ProcedureName.GetTargetedScoreInfoV4, new { codm });
        var result = targetedScores.Select(mapper.Map<TargetedScoreDto>).ToArray();
        return result;
    }

    public async Task<TargetedScoreDto[]> GetSubsistenceTargetedScoresByCodm(int codm) {
        var targetedScores = await dapper.ExecuteProcedureToList<TargetedScore>(ProcedureName.GetSubsistenceTargetedScoreInfoV4, new { codm });
        var result = targetedScores.Select(mapper.Map<TargetedScoreDto>).ToArray();
        return result;
    }

    public async Task<PictureHistoryDto[]> GetPictureHistoriesByCodm(int codm) {
        var pictureHistories = await dapper.ExecuteProcedureToList<PictureHistory>(ProcedureName.GetStudentPictureHistoryV4, new { codm });
        var result = pictureHistories.Select(mapper.Map<PictureHistoryDto>).ToArray();
        return result;
    }

    /// <summary>تمدید پرونده طلبه</summary>
    public async Task<ProcedureResultDto> ExtensionCaseCommand(StudentExtensionCaseCommandPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentCaseValidityDateAuto, command);
        return result;
    }

    /// <summary>تمدید پرونده کارمند-طلبه</summary>
    public async Task<ProcedureResultDto> ExtensionCaseCommand(ManualStudentExtensionCaseCommandPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentCaseValidityDate, command);
        return result;
    }

    /// <summary>تمدید پرونده عادی</summary>
    public async Task<ProcedureResultDto> EditCaseCommand(StudentNormalEditCaseCommandPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentCaseDescription, command);
        return result;
    }

    /// <summary>بروزرسانی تصویر پروفایل</summary>
    public async Task<ProcedureResultDto> UpdateProfilePictureCommand(UpdateStudentProfilePicturePrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentPictureV4, command);
        return result;
    }

    public async Task<ProcedureResultDto> SaveTemporaryProfilePicture(Guid fileId, byte[] picture) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentTmpPictureV4, new { fileId, picture });
        return result;
    }

    public async Task<byte[]> GetTempProfilePicture(Guid fileId) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<byte[]>(ProcedureName.GetStudentTmpPictureV4, new { fileId });
        return result;
    }

    public async Task<bool> IsDualStudentApprovalRequiredForAddress(StudentAddressApprovalRequestPrc query) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<bool>(ProcedureName.CheckAddressApproveV4, query);
        return result;
    }

    public async Task<GenerateOtpCodeProcedureResultDto> GenerateOtpCode(int codm, long? dependentId) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<GenerateOtpCodeProcedureResultDto>(ProcedureName.GenerateOtpCode, new { codm, dependentId });
        return result;
    }

    public async Task PrepareTestData() {
        _ = await dapper.ExecuteProcedureSingleOrDefault<int>(ProcedureName.PrepareTestData);
    }

    public async Task<ValidateStudentStatusForRegisterationResultDto> ValidateStudentStatusForRegistration(ValidateStudentStatusForRegistrationCommandPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ValidateStudentStatusForRegisterationResultDto>
            (ProcedureName.ValidateStudentStatusForRegisterationV4, command);
        return result;
    }

    public async Task<StudentRegistrationResultDto> CreateStudentRegistrationAsync(StudentRegistrationCommandPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentRegistrationResultDto>(ProcedureName.SetNewStudent, command);
        return result;
    }

    public async Task<StudentTaminInsuranceResultDto> GetTaminInsuranceByCodm(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentTaminInsuranceResultDto>(ProcedureName.GetTaminInsuranceInfoV4, new { codm });
        return result;
    }

    public async Task<ValidateStudentStatusForRegisterationResultDto> ValidateReligiousRoleQuestionByCodm(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ValidateStudentStatusForRegisterationResultDto>(ProcedureName.ValidateReligiousRoleV4, new { codm });
        return result;
    }

    public async Task<StudentPensionStatusDto> GetContinuousInformationTabByCodm(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentPensionStatusDto>(ProcedureName.GetPensionStatusV4, new { codm });
        return result;
    }

    public async Task<ProcedureResultDto> UpdateStudentSisterDivorceAsync(SetStudentSisterDivorceModel command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentDivorceV4, command);
        return result;
    }

    public async Task<EmployeeLastViewStudentLogDto[]> GetEmployeeLastViewStudentLogByPersonnelId(int personnelId) {
        var result = await dapper.ExecuteProcedureToList<EmployeeLastViewStudentLogDto>(ProcedureName.GetLastViewedCodm, new { @idkarbar = personnelId });
        return result.ToArray();
    }

    public async Task<StudentRecordCountDto> GetStudentRecordCount(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentRecordCountDto>(ProcedureName.GetTableRecordCountV4, new { codm });
        return result;
    }

    public async Task<StudentHouseHistoryDto> GetHouseHistory(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentHouseHistoryDto>(ProcedureName.GetHouseHistoryV4, new { codm });
        return result;
    }

    public async Task<StudentShahriehInfoDto> GetShahriehInfo(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentShahriehInfoDto>(ProcedureName.GetShahriehData, new { codm });
        return result;
    }

    public async Task<ProtestDto[]> GetProtests(int codm) {
        var result = await dapper.ExecuteProcedureToList<Protest>(ProcedureName.GetProtestPossibility, new { codm });
        return result.Select(mapper.Map<ProtestDto>).ToArray();
    }

    public async Task<StudentInfoNeedUpdateDto> GetStudentInfoNeedUpdateByCodm(int codm) {
        var result = await dapper.ExecuteProcedureBaseSingleOrDefault<StudentInfoNeedUpdateDto>(ProcedureName.GetMustUpdate, new { codm });
        return result;
    }

    //TODO موقت باید حذف شود
    //public async Task<List<StudentCardexShahriehDto>> GetStudentCardexShahrieh(GetStudentCardexShahriehQuery request) {
    //    var result = await dapper.ExecuteProcedureToList<StudentCardexShahriehDto>(ProcedureName.GetShahriehPayments, request);
    //    return result;
    //}

    public async Task<StudentTotalReportDto> GetStudentTarazAndLivelihoodTotalScore(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentTotalReportDto>(ProcedureName.GetTarazAndLivelihoodTotalScoreAndTotalScore, new { codm });
        return result;
    }

    public async Task UpdateBranchAndAgency(UpdateBranchAndAgencyRepoCommand command) {
        await dapper.ExecuteProcedure(ProcedureName.UpdateBranchAndAgency, command);
    }

    public async Task<GetUpdateWizardStepsVisibiltyDto> GetUpdateWizardStepsVisibilty(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<GetUpdateWizardStepsVisibiltyDto>(ProcedureName.GetUpdateWizardStepsVisibilty, new { codm });
        return result;
    }

    public async Task<CommissionStudentIdentityDto> GetCommissionForNewStudent(int commissionRequestId) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<CommissionStudentIdentityDto>
            (ProcedureName.GetCommissionForNewStudent, new { commissionRequestId });
        return result;
    }

    public async Task SetCommissionStatus(SetCommissionRequestRepoCommand command) {
        await dapper.ExecuteProcedure(ProcedureName.SetCommissionStatus, command);
    }

    public async Task<GetStudentEducationStatusDto> GetStudentEducationStatus(StudentEducationStatusRepoQuery query) {
        return await dapper.ExecuteProcedureSingleOrDefault<GetStudentEducationStatusDto>(ProcedureName.GetStudentEducationStatus, query);
    }

    public async Task UpdateNonIranianStudentCitizenship(UpdateNonIranianStudentCitizenshipRepoCommand command) {
        await dapper.ExecuteProcedure(ProcedureName.UpdateNonIranianStudentCitizenship, command);
    }

    public async Task UpdateNonIranianDependentCitizenship(UpdateNonIranianDependentCitizenshipRepoCommand command) {
        await dapper.ExecuteProcedure(ProcedureName.UpdateNonIranianDependentCitizenship, command);
    }

    public async Task UpdateStudentBirthCertInfo(UpdateStudentBirthCertInfoRepoCommand command) {
        await dapper.ExecuteProcedure(ProcedureName.SetStudentBirthCertInfo, command);
    }

    public async Task UpdateDependentBirthCertInfo(UpdateDependentBirthCertInfoRepoCommand command) {
        await dapper.ExecuteProcedure(ProcedureName.SetDependentBirthCertInfo, command);
    }

    public async Task<ProcedureResultDto> SetStudentBlocked(SetStudentBlockedRepoCommand command) {
        return await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentBlocked, command);
    }

    public async Task<ProcedureResultDto> SetStudentUnblocked(SetStudentUnBlockedRepoCommand command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentUnBlocked, command);
        return result;
    }

    public async Task<ProcedureResultDto> CreateStudentDeath(SetNonIranianStudentDeathPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentDeath, command);
        return result;
    }

    public async Task<ProcedureResultDto> SetDependentCaseDescription(CreateStudentDependentCaseDescriptionPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentCaseDescription, command);
        return result;
    }

    //public async Task UpdateNonIranianStudentBirthCertInfo(UpdateNonIranianStudentBirthCertRepoCommand command) {
    //    await _dapper.ExecuteProcedure(ProcedureName.UpdateNonIranianStudentIdentity, command);
    //}

    //public async Task UpdateNonIranianDependentBirthCertInfo(UpdateNonIranianDependentBirthCertRepoCommand command) {
    //    await _dapper.ExecuteProcedure(ProcedureName.UpdateNonIranianDependentIdentity, command);
    //}

    public async Task<ProcedureResultDto> UpdateDependentCaseActiveStatus(UpdateStudentDependentCaseActiveStatusPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentActive, command);
        return result;
    }

    public async Task<ProcedureResultDto> UpdateDependentCaseDeActiveStatus(UpdateStudentDependentCaseDeActiveStatusPrc command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentDeActive, command);
        return result;
    }

    public async Task<StudentDataForPayRunResult> GetDataForPayRunByCodm(int codm) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<StudentDataForPayRunResult>(ProcedureName.GetDataForPayRunByCodm, new { codm });
        return result;
    }

    public async Task<List<StudentDataForPayRunResult>> GetDataForPayRunByCodmList(string codmList) {
        var result = await dapper.ExecuteProcedureToList<StudentDataForPayRunResult>(ProcedureName.GetDataForPayRunByCodmList, new { codmList });
        return result;
    }

    public async Task<List<StudentDataForPayRunResult>> GetDataForPayRunByStartEndCodm(int startCodm, int endCodm) {
        var result = await dapper.ExecuteProcedureToList<StudentDataForPayRunResult>(ProcedureName.GetDataForPayRunByStartEnd, new { startCodm, endCodm });
        return result;
    }


    public async Task<ProcedureResultDto> SetStudentWithSabteAhvalData(SetStudentWithSabteAhvalDataRepoCommand command){
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentWithSabtAhvalData, command);
        return result;
    }

    public async Task<ProcedureResultDto> SetDependentWithSabteAhvalData(SetDependentWithSabteAhvalDataRepoCommand command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentWithSabtAhvalData, command);
        return result;
    }

    public async Task<ProcedureResultDto> SetStudentWithAlmostafaData(SetStudentWithAlmostafaDataRepoCommand command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentWithAlmostafaData, command);
        return result;
    }

    public async Task<ProcedureResultDto> SetDependentWithAlmostafaData(SetDependentWithAlmostafaDataRepoCommand command) {
        var result = await dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentWithAlmostafaData, command);
        return result;
    }
}
