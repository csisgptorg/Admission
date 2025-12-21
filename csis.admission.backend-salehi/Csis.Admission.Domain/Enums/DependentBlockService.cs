using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// خدمات مسدود تکفل
/// </summary>
public enum DependentBlockServiceEnum : short
{
    /// <summary> بن تکفل </summary>
    [Display(Name = "بن")]
    Voucher = 11,
    /// <summary> صندوق تکفل </summary>
    [Display(Name = "صندوق")]
    Fund = 12,
    /// <summary> بيمه درماني تکفل </summary>
    [Display(Name = "بيمه درماني")]
    HealthInsurance = 13,
    /// <summary> تامين اجتماعي تکفل </summary>
    [Display(Name = "تامين اجتماعي")]
    SocialSecurity = 14,
    /// <summary> مستمري بگيران تکفل </summary>
    [Display(Name = "مستمري بگيران")]
    Pensioners = 15,
    /// <summary> سهام عدالت تکفل </summary>
    [Display(Name = "سهام عدالت")]
    JusticeShares = 16,
    /// <summary> بيمه مکمل تکفل </summary>
    [Display(Name = "بيمه مکمل")]
    SupplementalInsurance = 17
}
