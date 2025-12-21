/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Settings;

/// <summary>
/// تنظیمات
/// </summary>
/// <typeparam name="TSettings">مدل مورد استفاده برای تنظیمات</typeparam>
public interface ISettings<TSettings> where TSettings : new()
{
    /// <summary>
    /// مقادیر پیش فرض تنظیمات
    /// </summary>
    /// <returns></returns>
    TSettings GetDefault();
}
