namespace Csis.Admission.Domain.Enums;

/// <summary>
/// دوره‌های مختلف سال تحصیلی
/// </summary>
public enum EducationSemester : short
{
    /// <summary>
    /// نیمسال اول.
    /// </summary>
    FirstSemester = 1,

    /// <summary>
    /// نیمسال دوم.
    /// </summary>
    SecondSemester = 2,

    /// <summary>
    /// دوره تابستانی.
    /// </summary>
    Summer = 3,

    /// <summary>
    /// دوره اختبار و تثبیت.
    /// </summary>
    Finalization = 4,

    /// <summary>
    /// کل سال تحصیلی.
    /// </summary>
    FullYear = 5
}
