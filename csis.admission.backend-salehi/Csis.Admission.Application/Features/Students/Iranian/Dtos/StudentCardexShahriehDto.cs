namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>کاردکس اطلاعات شهریه طلبه</summary>
public class StudentCardexShahriehDto
{
    /// <summary>کد شهریه</summary>
    public int Codm { get; set; }
    /// <summary>کد شهریه</summary>
    public string ShahriehCode { get; set; }
    /// <summary>تاریخ پرداخت</summary>
    public DateTime? PayDate { get; set; }
    /// <summary>مبلغ</summary>
    public long? Amount { get; set; }
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}

