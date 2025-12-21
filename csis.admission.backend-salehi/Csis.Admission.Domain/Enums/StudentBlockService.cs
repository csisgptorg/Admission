using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// خدمات مسدود طلبه
/// </summary>
public enum StudentBlockServiceEnum : short
{
    /// <summary> بن </summary>
    [Display(Name = "بن")]
    Voucher = 1,
    /// <summary> صندوق </summary>
    [Display(Name = "صندوق")]
    Fund = 2,
    /// <summary> مسکن </summary>
    [Display(Name = "مسکن")]
    Housing = 3,
    /// <summary> بيمه درماني </summary>
    [Display(Name = "بيمه درماني")]
    HealthInsurance = 4,
    /// <summary> تامين اجتماعي </summary>
    [Display(Name = "تامين اجتماعي")]
    SocialSecurity = 5,
    /// <summary> فروشگاه </summary>
    [Display(Name = "فروشگاه")]
    Store = 6,
    /// <summary> امداد و حوادث </summary>
    [Display(Name = "امداد و حوادث")]
    ReliefAndAccidents = 7,
    /// <summary> مستمري بگيران </summary>
    [Display(Name = "مستمري بگيران")]
    Pensioners = 8,
    /// <summary> هديه ازدواج </summary>
    [Display(Name = "هديه ازدواج")]
    MarriageGift = 9,
    /// <summary> سهام عدالت </summary>
    [Display(Name = "سهام عدالت")]
    JusticeShares = 10,
    /// <summary> بيمه مکمل </summary>
    [Display(Name = "بيمه مکمل")]
    SupplementalInsurance = 18
}
