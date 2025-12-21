using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع دانشگاه
/// </summary>
public enum UniversityTypeEnum : short
{
    /// <summary>
    /// دولتی
    /// </summary>
    [Display(Name = "دولتی")]
    Governmental = 1,

    /// <summary>
    /// آزاد
    /// </summary>
    [Display(Name = "آزاد")]
    AzadUniversity = 2,

    /// <summary>
    /// علمی کاربردی
    /// </summary>
    [Display(Name = "علمی کاربردی")]
    AppliedScience = 3,

    /// <summary>
    /// غیر انتفاعی
    /// </summary>
    [Display(Name = "غیر انتفاعی")]
    NonProfit = 4,

    /// <summary>
    /// پیام نور
    /// </summary>
    [Display(Name = "پیام نور")]
    LightMessage = 5,

    /// <summary>
    /// مجازی
    /// </summary>
    [Display(Name = "مجازی")]
    Virtual = 6,
}
