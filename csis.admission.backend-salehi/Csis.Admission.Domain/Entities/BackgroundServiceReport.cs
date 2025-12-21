/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت گزارش اجرای سرویس‌های پس زمینه
/// </summary>
public sealed class BackgroundServiceReport : BaseEntity
{
    /// <summary>
    /// نام سرویس
    /// </summary>
    public string ServiceTitle { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    public BackgroundServiceStatus Status { get; set; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public DateTime? FinishedOn { get; set; }

    /// <summary>
    /// زمان اجرای سرویس به میلی ثانیه
    /// </summary>
    public long ElapsedMilliseconds { get; set; }

    /// <summary>
    /// گزارش اجرای سرویس
    /// </summary>
    public string Report { get; set; }
}
