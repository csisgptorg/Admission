/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت نوتیفیکیشن
/// </summary>
public enum NotificationStatus : byte
{
    /// <summary>
    /// درانتظار ارسال
    /// </summary>
    Pending = 0,

    /// <summary>
    /// ارسال موفق به سرویس پیام رسان
    /// </summary>
    SuccessfullySentToNotificationService = 1,

    /// <summary>
    /// لغو شده
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// تحویل شده
    /// </summary>
    Delivered = 3,

    /// <summary>
    /// ناموفق
    /// </summary>
    Failed = 4,
}
