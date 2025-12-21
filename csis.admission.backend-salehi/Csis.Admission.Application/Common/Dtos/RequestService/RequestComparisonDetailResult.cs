namespace Csis.Admission.Application.Common.Dtos.RequestService;

/// <summary>
/// نتیجه کامل مقایسه درخواست - شامل داده فعلی + تغییرات درخواستی + لیست تفاوت‌ها
/// </summary>
public sealed class RequestComparisonDetailResult
{
    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; set; }

    /// <summary>نوع درخواست</summary>
    public RequestType RequestType { get; set; }

    /// <summary>
    /// داده‌های فعلی موجود در دیتابیس
    /// </summary>
    public object CurrentData { get; set; }

    /// <summary>
    /// داده‌های جدید درخواست شده (از Payload درخواست)
    /// </summary>
    public object RequestedChanges { get; set; }

    /// <summary>تاریخ ایجاد</summary>
    public string DateCreated { get; set; }

    /// <summary>زمان ایجاد</summary>
    public TimeSpan TimeCreated { get; set; }
    public ApprovalStatus Status { get; set; }
}
