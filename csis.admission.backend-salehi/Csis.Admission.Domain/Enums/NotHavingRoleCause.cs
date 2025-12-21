namespace Csis.Admission.Domain.Enums;

/// <summary>
/// دلیل عدم فعالیت
/// </summary>
public enum NotHavingRoleCause : short
{
    /// <summary>بیماری خاص و صعب العلاج</summary>
    SpecialAndIncurableDisease = 1,

    /// <summary>بیماری خاص و صعب العلاج آشنایان</summary>
    SpecialAndIncurableDiseaseAffiliationAffiliation = 2,

    /// <summary>مسائل و مشکلات مالی</summary>
    FinancialIssuesAndProblems = 3,

    /// <summary>آسیب های اجتماعی (اعتیاد, طلاق, زندان, ...)</summary>
    SocialHarms = 4,

    /// <summary>عدم علاقه به فعالیت های حوزوی</summary>
    LackInterestSeminaryActivities = 5,

    /// <summary>خروج از حوزه</summary>
    ExiFromTheAcademicField = 6,

    /// <summary>اشتغال به کار</summary>
    Employment = 7,
    
    /// <summary>سایر</summary>
    Other = 8,
}

