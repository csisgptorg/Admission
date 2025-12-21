namespace Csis.Admission.Application.Common.Dtos.RequestService;

/// <summary>کاربر جاری</summary>
public class CurrentUserDto
{
    /// <summary>شناسه</summary>
    public int? Id { get; set; }

    /// <summary>کد مرکز خدمات</summary>
    public int? Codm { get; set; }

    /// <summary>طلبه است</summary>
    public bool IsStudent { get; set; } = false;

    /// <summary>شناسه پرسنلی</summary>
    public int? PersonnelId { get; set; }

    /// <summary>کارمند است</summary>
    public bool IsEmployee { get; set; } = false;

    /// <summary>نام و نام خانوادگی</summary>
    public string FullName { get; set; }

    /// <summary>کارمند ارشد</summary>
    public bool IsSenior { get; set; }
}
