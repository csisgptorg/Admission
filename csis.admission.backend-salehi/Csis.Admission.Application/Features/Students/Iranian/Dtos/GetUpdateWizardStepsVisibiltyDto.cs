namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>گام هایی قابل نمایش در ویزارد بیروز رسانی اطلاعات طلبه</summary>
public record GetUpdateWizardStepsVisibiltyDto
{
    /// <summary>نمایش تصویر پروفایل</summary>
    public bool PictureVisibility { get; set; }

    /// <summary>نمایش اطلاعات شغلی</summary>
    public bool EmploymentVisibility { get; set; }

    /// <summary>نمایش اطلاعات محل سکونت</summary>
    public bool HouseVisibility { get; set; }

    /// <summary>نمایش اطلاعات آدرس</summary>
    public bool AddressVisibility { get; set; }
}
