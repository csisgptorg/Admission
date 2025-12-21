using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>اطلاعات پرونده ای تکفل</summary>
public class DependentCase : BaseEntity<long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>وضعیت فعال بودن</summary>
    public bool IsActive { get; set; }
    /// <summary>تاریخ تشکیل پرونده</summary>
    public int CaseCreateDate { get; set; }
    /// <summary>تاریخ اعتبار پرونده</summary>
    public int? CaseDeactiveDate { get; set; }
    /// <summary>دلیل وضعیت غیرفعال/فعال</summary>
    public string StatusReason { get; init; }
    /// <summary>عنوان وضعیت غیرفعال/فعال</summary>
    public string StatusReasonTitle { get; init; }
    /// <summary>توضیحات</summary>
    public string CaseDescription { get; init; }
    /// <summary>کد سرپرست کنونی - کد سرپرست تکفل تغییر کرده</summary>
    public int? TrasferedTo { get; set; }
    /// <summary>کد مستقل - تکفل بوده و الان خودش طلبه شده و کد گرفته</summary>
    public int? AsStudent { get; set; }
    /// <summary>تاریخ انقضا</summary>
    public int DateExpire { get; set; }
    /// <summary>علت انقضای پرونده</summary>
    public string ReasonOfExpiration { get; set; }
}
