namespace Csis.Admission.Application.Enums;

/// <summary>
/// انواع Entity برای مقایسه داده‌ها
/// </summary>
public enum ComparisonEntityType
{
    // ===== EF Core Entities =====

    /// <summary>اطلاعات شغلی طلبه</summary>
    GetStudentEmploymentByCodmQuery = 1,

    /// <summary>اطلاعات شغلی تکفل</summary>
    GetDependentEmploymentByCodmQuery = 2,

    /// <summary>اطلاعات مسکن</summary>
    GetHouseByCodmQuery = 3,

    /// <summary>اطلاعات تحصیلی</summary>
    GetEducationByCodmQuery = 4,

    /// <summary>اطلاعات تحصیلات دانشگاهی</summary>
    GetUniversityEducationByCodmQuery = 5,

    /// <summary>اطلاعات جانبازی</summary>
    GetVeteranByCodmQuery = 6,

    /// <summary>اطلاعات نخبگی</summary>
    GetEliteByCodmQuery = 7,

    /// <summary>اطلاعات حافظ قرآن</summary>
    GetMemorizerByCodmQuery = 8,

    /// <summary>اطلاعات تبلیغ</summary>
    GetPreachByCodmQuery = 9,

    /// <summary>اطلاعات شهرت</summary>
    GetFamousByCodmQuery = 10,

    /// <summary>اطلاعات درجه پژوهش</summary>
    GetResearchGradeByCodmQuery = 11,

    /// <summary>اطلاعات درجه تبلیغ</summary>
    GetPreachGradeByCodmQuery = 12,

    /// <summary>اطلاعات دوست طلبه</summary>
    GetStudentFriendByCodmQuery = 13,

    /// <summary>اطلاعات امامت جماعت</summary>
    GetImamJamaatByCodmQuery = 14,

    /// <summary>اطلاعات فعالیت فرهنگی</summary>
    GetCulturalActivityByCodmQuery = 15,

    /// <summary>اطلاعات تدریس</summary>
    GetTeachByCodmQuery = 16,

    /// <summary>اطلاعات پژوهش</summary>
    GetResearchByCodmQuery = 17,

    /// <summary>درجه تدریس</summary>
    GetTeachGradeByCodmQuery = 18,

    /// <summary>درجه فعالیت فرهنگی</summary>
    GetCulturalActivityGradeByCodmQuery = 19,

    // ===== Dapper Entities =====

    /// <summary>اطلاعات خلاصه طلبه (Dapper)</summary>
    GetStudentSummaryByCodmQuery = 100,

    /// <summary>حساب بانکی طلبه (Dapper)</summary>
    GetStudentBankAccountByCodmQuery = 101,

    /// <summary>حساب بانکی تکفل (Dapper)</summary>
    GetDependentBankAccountByCodmQuery = 102,

    /// <summary>آدرس طلبه (Dapper)</summary>
    GetAddressesByCodmQuery = 103,

    /// <summary>شماره تماس طلبه (Dapper)</summary>
    GetStudentMobileByCodmQuery = 104,

    /// <summary>شماره تماس تکفل (Dapper)</summary>
    GetDependentMobileByCodmQuery = 105,

    /// <summary>خلاصه اطلاعات تکفل (Dapper)</summary>
    GetDependentSummaryByCodmQuery = 106,

    /// <summary>تصویر پروفایل طلبه (Dapper)</summary>
    GetStudentProfileImageByCodmQuery = 107,

    /// <summary>اطلاعات تکفل (Dapper)</summary>
    GetStudentDependentByCodmQuery = 108,
}
