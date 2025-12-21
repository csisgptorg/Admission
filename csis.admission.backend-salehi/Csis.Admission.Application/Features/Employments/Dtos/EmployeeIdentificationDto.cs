using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Employments.Dtos;

/// <summary>
/// اشتغال موردی
/// </summary>
public record EmployeeIdentificationDto : BaseDto<EmployeeIdentificationDto, EmployeeIdentification>
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نام محل کار
    /// </summary>
    public string EmployeeName { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// آیا فرآیند تکمیل شده است؟
    /// </summary>
    public bool IsFinish { get; set; }
}
