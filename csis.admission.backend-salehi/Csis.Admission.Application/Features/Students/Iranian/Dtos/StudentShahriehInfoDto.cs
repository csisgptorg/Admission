namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>اطلاعات شهریه</summary>
public record StudentShahriehInfoDto
{
    /// <summary>کد شهریه</summary>
    public int ShahriehCode { get; set; }
    /// <summary>وضعیت</summary>
    public string Status { get; set; }
    /// <summary>رتبه</summary>
    public string Rank { get; set; }
    /// <summary>مبلغ</summary>
    public decimal Amount { get; set; }
}
