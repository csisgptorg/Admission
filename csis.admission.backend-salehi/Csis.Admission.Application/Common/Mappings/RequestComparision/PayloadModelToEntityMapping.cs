using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Common.Mappings.RequestComparision;

/// <summary>
/// نگاشت بین PayloadModel (نام Command) و نوع Entity مربوطه
/// این Dictionary برای دریافت پویا Repository در زمان اجرا استفاده می‌شود
/// </summary>
public static class PayloadModelToEntityMapping
{
    /// <summary>
    /// نگاشت PayloadModel به نوع Entity
    /// Key: نام Command (مثل "UpdateStudentMobileCommand")
    /// Value: نوع Entity (مثل typeof(StudentSummary))
    /// </summary>
    public static readonly Dictionary<string, Type> Mapping = new(StringComparer.OrdinalIgnoreCase)
    {
        // Mobile & Phone
        ["UpdateStudentPhoneCommand"] = typeof(StudentSummary),
        ["UpdateStudentMobileCommand"] = typeof(StudentSummary),
        ["UpdateDependentMobileCommand"] = typeof(DependentSummary),
        ["UpdateStudentMobileRepoCommand"] = typeof(StudentSummary),
        
        // Bank Account
        ["UpdateStudentBankAccountCommand"] = typeof(StudentSummary),
        ["UpdateDependentBankAccountCommand"] = typeof(DependentSummary),
        
        // Address
        ["CreateOrUpdateStudentAddressCommand"] = typeof(Address),
        ["CreateOrUpdateStudentAddressEmployeeCommand"] = typeof(Address),
        
        // Employment
        ["CreateOrUpdateStudentEmploymentCommand"] = typeof(StudentEmployment),
        ["CreateOrUpdateDependentEmploymentCommand"] = typeof(DependentEmployment),
        ["IdentifyStudentEmploymentCommand"] = typeof(StudentEmployment),
        ["DeleteStudentEmploymentCommand"] = typeof(StudentEmployment),
        ["DeleteDependentEmploymentCommand"] = typeof(DependentEmployment),
        
        // House
        ["CreateOrUpdateHouseCommand"] = typeof(House),
        ["DeleteHouseCommand"] = typeof(House),
        
        // Education
        ["UpdateEducationCommand"] = typeof(Education),
        ["CreateStudentUniversityEducationNonIranianCommand"] = typeof(UniversityEducation),
        ["CreateStudentUniversityEducationIranianCommand"] = typeof(UniversityEducation),
        ["CreateDependentUniversityEducationNonIranianCommand"] = typeof(UniversityEducation),
        ["CreateDependentUniversityEducationIranianCommand"] = typeof(UniversityEducation),
        ["DeleteStudentUniversityEducationCommand"] = typeof(UniversityEducation),
        ["DeleteDependentUniversityEducationCommand"] = typeof(UniversityEducation),
        
        // Veteran & Elite & Memorizer
        ["CreateOrUpdateVeteranCommand"] = typeof(Veteran),
        ["DeleteVeteranCommand"] = typeof(Veteran),
        ["CreateOrUpdateEliteCommand"] = typeof(Elite),
        ["DeleteEliteCommand"] = typeof(Elite),
        ["DeleteMemorizerCommand"] = typeof(Memorizer),
        
        // Cultural Activities
        ["CreatePreachCommand"] = typeof(Preach),
        ["UpdatePreachCommand"] = typeof(Preach),
        ["DeletePreachCommand"] = typeof(Preach),
        ["CreateFamousCommand"] = typeof(Famous),
        ["UpdateFamousCommand"] = typeof(Famous),
        ["DeleteFamousCommand"] = typeof(Famous),
        ["CreateTeachCommand"] = typeof(Teach),
        ["UpdateTeachCommand"] = typeof(Teach),
        ["DeleteTeachCommand"] = typeof(Teach),
        ["CreateResearchCommand"] = typeof(Research),
        ["UpdateResearchCommand"] = typeof(Research),
        ["DeleteResearchCommand"] = typeof(Research),
        
        // Grades
        ["DeleteCulturalActivityGradeCommand"] = typeof(CulturalActivityGrade),
        ["DeletePreachGradeCommand"] = typeof(PreachGrade),
        ["DeleteResearchGradeCommand"] = typeof(ResearchGrade),
        ["DeleteTeachGradeCommand"] = typeof(TeachGrade),
        
        // Student Friend
        ["CreateStudentFriendCommand"] = typeof(StudentFriend),
        ["DeleteStudentFriendCommand"] = typeof(StudentFriend),
        
        // Profile & Summary
        ["UpdateStudentProfilePictureCommand"] = typeof(StudentSummary),
        ["CompleteInformationCaseFilingCommand"] = typeof(StudentSummary),
        ["CreateAdmissionCaseCommand"] = typeof(StudentSummary),
        
        // Dependent
        ["RegisterSupportCommand"] = typeof(DependentSummary),
        ["StudentSpouseRegistryCommand"] = typeof(DependentSummary),
        ["UpdateChildMarriageCommand"] = typeof(DependentSummary),
        ["UpdateStudentSisterMarriageCommand"] = typeof(DependentSummary),
        ["UpdateDependentDivorceCommand"] = typeof(DependentSummary),
        ["UpdateStudentSisterDivorceCommand"] = typeof(StudentSummary),
        ["UpdateWifeDivorceCommand"] = typeof(DependentSummary),
        ["UpdateNonIranianDependentMarriageCommand"] = typeof(DependentSummary),
        ["UpdateNonIranianWifeMarriageCommand"] = typeof(DependentSummary),
        ["UpdateNonIranianDependentDivorceCommand"] = typeof(DependentSummary),
        ["UpdateNonIranianWifeDivorceCommand"] = typeof(DependentSummary),
        ["CreateStudentDependentCaseDescriptionCommand"] = typeof(DependentSummary),
        ["UpdateDependentCaseActiveSeniorCommand"] = typeof(DependentSummary),
        ["UpdateDependentCaseDeActiveSeniorCommand"] = typeof(DependentSummary),
        ["UpdateDependentCaseActiveEmployeeCommand"] = typeof(DependentSummary),
    };

    /// <summary>
    /// دریافت نوع Entity بر اساس PayloadModel
    /// </summary>
    /// <param name="payloadModel">نام Command (مثل "UpdateStudentMobileCommand")</param>
    /// <returns>نوع Entity یا null اگر یافت نشد</returns>
    public static Type GetEntityType(string payloadModel)
    {
        if (string.IsNullOrWhiteSpace(payloadModel))
        {
            return null;
        }

        // حذف پسوند های اضافی
        var cleanedName = payloadModel
            .Replace("RequestCommand", "Command")
            .Replace("RepoCommand", "Command");

        return Mapping.TryGetValue(cleanedName, out var entityType) ? entityType : null;
    }
}
