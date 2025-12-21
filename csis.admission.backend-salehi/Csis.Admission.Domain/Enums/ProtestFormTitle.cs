using System.ComponentModel.DataAnnotations;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Csis.Admission.Domain.Enums;

/// <summary>عنوان فرم های اعتراضات</summary>
public enum ProtestFormTitle : short
{
    None = 0,
    [Display(Name = "فرم جیم")]
    FormJim = 1,

    [Display(Name = "سابقه مالکیت در سازمان ثبت اسناد")]
    OwnershipHistory = 2,

    [Display(Name = "سابقه دریافت تسهیلات از بانک مسکن")]
    HousingLoanHistory = 3,

    [Display(Name = "سابقه موجر بودن")]
    BeingLandlord = 4,

    [Display(Name = "سابقه خرید و فروش مسکن")]
    HousingBuySellHistory = 5,

    [Display(Name = "سابقه مسکن شخصی در پذیرش")]
    PersonalHousingHistory = 6,

    [Display(Name = "شناسایی اشتغال از طریق سامانه‌ها")]
    EmploymentIdentificationSystems = 8,

    [Display(Name = "دهک معیشتی")]
    LivelihoodDecile = 9,

    [Display(Name = "اشتغال تکفل")]
    EmploymentSupport = 10
}
