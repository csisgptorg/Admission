namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع مشهور بودن
/// </summary>
public enum FamousType : byte
{
    /// <summary>
    /// برزرگان حوزه و نظام
    /// </summary>
    ProminentFigures = 1,

    /// <summary>
    /// علمای بلاد
    /// </summary>
    RegionalScholars = 2,

    /// <summary>
    /// مبلغ مشهور
    /// </summary>
    FamousPreacher = 3,

    /// <summary>
    /// پژوهشگر مشهور
    /// </summary>
    FamousResearcher = 4,

    /// <summary>
    /// استاد مشهور
    /// </summary>
    FamousTeacher = 5,

    /// <summary>
    /// روحانیون خاص
    /// </summary>
    SpecialClerics = 6
}
