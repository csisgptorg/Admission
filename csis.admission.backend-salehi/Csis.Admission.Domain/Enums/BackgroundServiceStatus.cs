/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت اجرای سرویس پس زمینه
/// </summary>
public enum BackgroundServiceStatus : byte
{
    /// <summary>
    /// درحال اجرا
    /// </summary>
    Running = 0,

    /// <summary>
    /// پایان یافته
    /// </summary>
    Finished = 1,

    /// <summary>
    /// خطا
    /// </summary>
    Error = 2,

    /// <summary>
    /// توقف اجباری
    /// </summary>
    ForceStopped = 3
}
