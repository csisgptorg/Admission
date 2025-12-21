using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>وضعیت سکونت</summary>
public enum HouseStatus : short
{
    /// <summary>اجاره ای یا رهنی</summary>
    [Display(Name = "اجاره ای یا رهنی")]
    RentalOrMortgage = 2,

    /// <summary>شخصی</summary>
    [Display(Name = "شخصی")]
    Private = 5,

    /// <summary>حمایتی</summary>
    [Display(Name = "حمایتی")]
    Supportive = 11
}
