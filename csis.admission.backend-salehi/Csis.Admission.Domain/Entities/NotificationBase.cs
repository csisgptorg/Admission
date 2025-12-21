/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت سابقه ارسال نوتیفیکیشن
/// </summary>
public sealed partial class Notification : BaseEntity, IFilterable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int? Codm { get; init; }

    /// <summary>
    /// کد پرسنلی
    /// </summary>
    public int? PersonnelId { get; init; }

    /// <summary>
    /// قالب نوتیفیکیشن
    /// </summary>
    public string Template { get; init; }

    /// <summary>
    /// متن نوتیفیکیشن
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// کانال‌های ارسال
    /// </summary>
    public List<int> DeliveryChannels { get; init; }

    /// <summary>
    /// تاریخ ارسال موفق
    /// </summary>
    public DateTime? SentOn { get; private set; }

    /// <summary>
    /// تاریخ آخرین پردازش
    /// </summary>
    public DateTime? LastProcessedOn { get; private set; }

    /// <summary>
    /// تاریخ زمانبندی ارسال
    /// </summary>
    public DateTime? ScheduleDate { get; init; }

    /// <summary>
    /// تعداد تلاش جهت ارسال
    /// </summary>
    public byte TriesCount { get; private set; }

    /// <summary>
    /// نوع نوتیفیکیشن
    /// </summary>
    public NotificationType Type { get; init; }

    /// <summary>
    /// وضعیت ارسال
    /// </summary>
    public NotificationStatus Status { get; private set; } = NotificationStatus.Pending;

    /// <summary>
    /// اولویت ارسال
    /// </summary>
    public NotificationPriority Priority { get; init; }

    /// <summary>
    /// شناسه پیام در سرویس پیام رسان
    /// </summary>
    public long? MessageId { get; private set; }

    /// <summary>
    /// ارسال موفق
    /// </summary>
    /// <param name="date"></param>
    /// <param name="messageId">شناسه پیام در سرویس پیام رسان</param>
    public void SendSuccessfully(DateTime date, long messageId) {
        TriesCount++;
        SentOn = date;
        LastProcessedOn = date;
        Status = NotificationStatus.SuccessfullySentToNotificationService;
        MessageId = messageId;
    }

    /// <summary>
    /// ارسال ناموفق
    /// </summary>
    /// <param name="date"></param>
    public void SendFailed(DateTime date) {
        TriesCount++;
        LastProcessedOn = date;
    }

    /// <summary>
    /// لغو
    /// </summary>
    public void Cancel() {
        Status = NotificationStatus.Cancelled;
    }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [.. _customFilterableFields.Concat([
            nameof(Codm),
            nameof(TriesCount),
            nameof(SentOn),
            nameof(LastProcessedOn),
            nameof(Status),
            nameof(Type),
            nameof(MessageId)
        ])];
    }
}
