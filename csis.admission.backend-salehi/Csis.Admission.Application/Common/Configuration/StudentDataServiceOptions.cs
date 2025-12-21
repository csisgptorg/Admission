/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// مدل تنظیمات سرویس دریافت اطلاعات طلاب
/// </summary>
public sealed class StudentDataServiceOptions
{
    /// <summary>
    /// آدرس پایه
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// کلید دسترسی
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// تایم اوت
    /// </summary>
    public int TimeoutInSeconds { get; set; }
}
