using Csis.Admission.Application.Features.BankAccounts.Queries;
using Csis.Admission.Application.Features.CulturalActivityGrades.Queries;
using Csis.Admission.Application.Features.DependentEmployments.Queries;
using Csis.Admission.Application.Features.Divorce.Queries;
using Csis.Admission.Application.Features.Educations.Queries;
using Csis.Admission.Application.Features.Elites.Queries;
using Csis.Admission.Application.Features.Employments.Queries;
using Csis.Admission.Application.Features.Famouses.Queries;
using Csis.Admission.Application.Features.Houses.Queries;
using Csis.Admission.Application.Features.Memorizers.Queries;
using Csis.Admission.Application.Features.Preaches.Queries;
using Csis.Admission.Application.Features.PreachGrades.Queries;
using Csis.Admission.Application.Features.Researches.Queries;
using Csis.Admission.Application.Features.ResearchGrades.Queries;
using Csis.Admission.Application.Features.StudentDependents.Queries;
using Csis.Admission.Application.Features.StudentFriends.Queries;
using Csis.Admission.Application.Features.StudentMobiles.Queries;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Application.Features.Teaches.Queries;
using Csis.Admission.Application.Features.TeachGrades.Queries;
using Csis.Admission.Application.Features.UniversityEducations.Queries;
using Csis.Admission.Application.Features.Veterans.Queries;

namespace Csis.Admission.Application.Common.Mappings.RequestComparision;

/// <summary>
/// نگاشت بین RequestType و نوع Query مربوطه
/// این Dictionary برای ایجاد پویای Query ها در زمان اجرا استفاده می‌شود
/// </summary>
public static class RequestTypeMapping
{
    /// <summary>
    /// نگاشت RequestType به نوع Query
    /// مقدار Codm در زمان اجرا به صورت پویا تزریق می‌شود
    /// </summary>
    public static readonly Dictionary<RequestType, Type> QueryTypeMapping = new()
    {
        // Mobile & Phone
        [RequestType.UpdateStudentMobile] = typeof(GetFamilyMobilesByCodmQuery),
        [RequestType.UpdateDependentMobile] = typeof(GetFamilyMobilesByCodmQuery),
        [RequestType.UpdateStudentPhone] = typeof(GetFamilyMobilesByCodmQuery),
        [RequestType.UpdateStudentMobileRepo] = typeof(GetFamilyMobilesByCodmQuery),

        // Bank Account
        [RequestType.UpdateStudentBankAccount] = typeof(GetFamilyBankAccountsByCodmQuery),
        [RequestType.UpdateDependentBankAccount] = typeof(GetFamilyBankAccountsByCodmQuery),

        // Address
        [RequestType.CreateOrUpdateStudentAddress] = typeof(GetStudentAddressByCodmQuery),
        [RequestType.CreateOrUpdateStudentAddressEmployee] = typeof(GetStudentAddressByCodmQuery),

        // Employment
        [RequestType.CreateOrUpdateStudentEmployment] = typeof(GetStudentEmploymentByCodmQuery),
        [RequestType.CreateOrUpdateDependentEmployment] = typeof(GetDependentsEmploymentByCodmQuery),
        [RequestType.IdentifyStudentEmployment] = typeof(GetStudentEmploymentByCodmQuery),
        [RequestType.DeleteStudentEmployment] = typeof(GetStudentEmploymentByCodmQuery),
        [RequestType.DeleteDependentEmployment] = typeof(GetDependentsEmploymentByCodmQuery),

        // House
        [RequestType.CreateOrUpdateHouse] = typeof(GetHouseByCodmQuery),
        [RequestType.DeleteHouse] = typeof(GetHouseByCodmQuery),

        // Education
        [RequestType.Education] = typeof(GetEducationByCodmQuery),
        [RequestType.CreateStudentUniversityEducationNonIranian] = typeof(GetStudentUniversityEducationsByCodmQuery),
        [RequestType.CreateStudentUniversityEducationIranian] = typeof(GetStudentUniversityEducationsByCodmQuery),
        [RequestType.CreateDependentUniversityEducationNonIranian] = typeof(GetDependentUniversityEducationsByCodmQuery),
        [RequestType.CreateDependentUniversityEducationIranian] = typeof(GetDependentUniversityEducationsByCodmQuery),
        [RequestType.DeleteStudentUniversityEducation] = typeof(GetStudentUniversityEducationsByCodmQuery),
        [RequestType.DeleteDependentUniversityEducation] = typeof(GetDependentUniversityEducationsByCodmQuery),

        // Veteran & Elite & Memorizer
        [RequestType.CreateOrUpdateVeteran] = typeof(GetVeteranByCodmQuery),
        [RequestType.DeleteVeteran] = typeof(GetVeteranByCodmQuery),
        [RequestType.CreateOrUpdateElite] = typeof(GetElitesByCodmQuery),
        [RequestType.DeleteElite] = typeof(GetElitesByCodmQuery),
        [RequestType.DeleteMemorizer] = typeof(GetStudentMemorizerByCodmQuery),

        // Cultural Activities
        [RequestType.CreatePreach] = typeof(GetPreachesByCodmQuery),
        [RequestType.UpdatePreach] = typeof(GetPreachesByCodmQuery),
        [RequestType.DeletePreach] = typeof(GetPreachesByCodmQuery),
        [RequestType.CreateFamous] = typeof(GetFamousByCodmQuery),
        [RequestType.UpdateFamous] = typeof(GetFamousByCodmQuery),
        [RequestType.DeleteFamous] = typeof(GetFamousByCodmQuery),
        [RequestType.CreateTeach] = typeof(GetTeachesByCodmQuery),
        [RequestType.UpdateTeach] = typeof(GetTeachesByCodmQuery),
        [RequestType.DeleteTeach] = typeof(GetTeachesByCodmQuery),
        [RequestType.CreateResearch] = typeof(GetResearchesByCodmQuery),
        [RequestType.UpdateResearch] = typeof(GetResearchesByCodmQuery),
        [RequestType.DeleteResearch] = typeof(GetResearchesByCodmQuery),

        // Grades
        [RequestType.DeleteCulturalActivityGrade] = typeof(GetCulturalActivityGradesByCodmQuery),
        [RequestType.DeletePreachGrade] = typeof(GetPreachGradesByCodmQuery),
        [RequestType.DeleteResearchGrade] = typeof(GetResearchGradesByCodmQuery),
        [RequestType.DeleteTeachGrade] = typeof(GetTeachGradesByCodmQuery),

        // Student Friend
        [RequestType.CreateStudentFriend] = typeof(GetStudentFriendByCodmQuery),
        [RequestType.DeleteStudentFriend] = typeof(GetStudentFriendByCodmQuery),

        // Profile & Summary
        [RequestType.UpdateStudentProfilePicture] = typeof(GetStudentProfileImageByCodmQuery),
        [RequestType.CompleteInformationCaseFiling] = typeof(GetStudentSummaryCaseByCodmQuery),
        [RequestType.CreateAdmissionCase] = typeof(GetStudentSummaryCaseByCodmQuery),

        // Dependent
        [RequestType.RegisterSupport] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.StudentSpouseRegistry] = typeof(GetDependentSpousesQuery),
        [RequestType.UpdateChildMarriage] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateStudentSisterMarriage] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateDependentDivorce] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateStudentSisterDivorce] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateWifeDivorce] = typeof(GetDependentSpousesDivorceQuery),
        [RequestType.UpdateNonIranianDependentMarriage] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateNonIranianWifeMarriage] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateNonIranianDependentDivorce] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateNonIranianWifeDivorce] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.CreateStudentDependentCaseDescription] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateDependentCaseActiveSenior] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateDependentCaseDeActiveSenior] = typeof(GetStudentDependentsByStudentCodmQuery),
        [RequestType.UpdateDependentCaseActiveEmployee] = typeof(GetStudentDependentsByStudentCodmQuery),
    };
}
