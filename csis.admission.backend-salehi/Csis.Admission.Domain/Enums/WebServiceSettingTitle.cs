using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// عنوان تنظیمات وب سرویس
/// </summary>
public enum WebServiceSettingTitle : short
{
    /// <summary>
    /// برای ایرانی ها
    /// </summary>
    [Display(Name = "Iranian")]
    Iranian = 1,
    /// برای غیر ایرانی ها
    [Display(Name = "NonIranian")]
    NonIranian = 2,
}
