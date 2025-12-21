namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>سابقه مسکن</summary>
public record StudentHouseHistoryDto
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>فرم جیم راه و شهرسازی</summary>
    public string JimForm { get; set; }
    /// <summary>سازمان ثبت اسناد</summary>
    public string SabtAsnadOffice { get; set; }
    /// <summary>سابقه دریافت تسهیلات از بانک مسکن</summary>
    public string MaskanBank { get; set; }
    /// <summary>موجر</summary>
    public string LandLord { get; set; }
    /// <summary>سابقه خرید یا فروش مسکن</summary>
    public string HasHouseDeal { get; set; }
    /// <summary>سابقه مسکن شخصی در پذیرش</summary>
    public string HasHouseStatusLog { get; set; }
    /// <summary>دریافت خدمات مسکن از مرکز خدمات</summary>
    public string MaskanMarkaz { get; set; }
    /// <summary>توضیحات مسکن پذیرش</summary>
    public string MaskanMarkazDescription { get; set; }
}
