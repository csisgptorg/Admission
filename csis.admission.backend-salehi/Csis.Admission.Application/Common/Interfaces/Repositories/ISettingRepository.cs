/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// مخزن تنظیمات
/// </summary>
public interface ISettingRepository
{
    /// <summary>
    /// دریافت تنظیمات با کلید
    /// </summary>
    /// <param name="key">کلید تنظیمات</param>
    /// <returns></returns>
    Task<Setting> GetByKeyAsync(string key);

    /// <summary>
    /// دریافت تنظیمات با کلید
    /// </summary>
    /// <param name="key">کلید تنظیمات</param>
    /// <returns></returns>
    Task<Setting> GetByKeyAsTrackingAsync(string key);

    /// <summary>
    /// ذخیره تنظیمات جدید
    /// </summary>
    /// <param name="setting">تنظیمات</param>
    /// <returns></returns>
    Task InsertAsync(Setting setting);

    /// <summary>
    /// بروزرسانی تنظیمات
    /// </summary>
    /// <param name="setting">تنظیمات</param>
    /// <returns></returns>
    Task UpdateAsync(Setting setting);
}
