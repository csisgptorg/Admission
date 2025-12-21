namespace Csis.Admission.Domain.Enums;

/// <summary>
/// انواع شهدا بر اساس نوع ایثارگری
/// </summary>
public enum MartyrType:short
{
    /// <summary>
    /// شهداي انقلاب اسلامی
    /// </summary>
    Revolution = 1,

    /// <summary>
    /// شهداي دفاع مقدس
    /// </summary>
    SacredDefense = 2,

    /// <summary>
    /// شهداي حوادث تروريستي
    /// </summary>
    TerroristIncidents = 3,

    /// <summary>
    /// شهداي مدافع حرم
    /// </summary>
    ShrineDefender = 4,

    /// <summary>
    /// شهداي مدافع امنيت
    /// </summary>
    SecurityDefender = 5
}
