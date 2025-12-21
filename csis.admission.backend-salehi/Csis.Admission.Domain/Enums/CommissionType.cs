namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع کمسیون
/// </summary>
public enum CommissionType : short
{
    /// <summary>
    /// خطبا
    /// </summary>
    Khotaba = 1,

    /// <summary>
    /// ملامحلی
    /// </summary>
    MalaMahalli = 2,

    /// <summary>
    /// مدارس غیر تحت پوشش
    /// </summary>
    NonCoveredSchools = 3,

    /// <summary>
    /// بالای پنجاه سال
    /// </summary>
    OverFiftyYears = 4,

    /// <summary>
    /// در نوبت کمیسیون - بدون مرجع
    /// </summary>
    PendingCommission = 5,

    /// <summary>
    /// مرحوم
    /// </summary>
    Deceased = 6,

    /// <summary>
    /// طلاب آزاد غیر ایرانی
    /// </summary>
    FreeNonIranianStudents = 7,

    /// <summary>
    /// کد تسهیلاتی جامعه المصطفی
    /// </summary>
    MustafaCommunityCode = 8
}
