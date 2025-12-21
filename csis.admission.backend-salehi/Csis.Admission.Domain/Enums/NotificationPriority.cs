/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// اولویت ارسال نوتیفیکیشن
/// </summary>
public enum NotificationPriority : byte
{
    /// <summary>
    /// پایین
    /// </summary>
    Low = 0,

    /// <summary>
    /// عادی
    /// </summary>
    Normal = 1,

    /// <summary>
    /// بالا
    /// </summary>
    High = 2
}
