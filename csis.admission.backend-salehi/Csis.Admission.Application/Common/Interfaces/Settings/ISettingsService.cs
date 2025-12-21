/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Models;
using Csis.Admission.Domain.Settings;

namespace Csis.Admission.Application.Common.Interfaces.Settings;

/// <summary>
/// سرویس تنظیمات
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// خواندن تنظیمات با کلید
    /// </summary>
    /// <typeparam name="TSettings">مدل تنظیمات</typeparam>
    /// <param name="keySuffix">پسوند کلید تنظیمات</param>
    /// <returns></returns>
    Task<SettingsModel<TSettings>> GetAsync<TSettings>(string keySuffix) where TSettings : ISettings<TSettings>, new();

    /// <summary>
    /// خواندن تنظیمات
    /// </summary>
    /// <typeparam name="TSettings">مدل تنظیمات</typeparam>
    /// <returns></returns>
    Task<SettingsModel<TSettings>> GetAsync<TSettings>() where TSettings : ISettings<TSettings>, new();

    /// <summary>
    /// ذخیره تنظیمات با کلید
    /// </summary>
    /// <typeparam name="TSettings">مدل تنظیمات</typeparam>
    /// <param name="keySuffix">پسوند کلید تنظیمات</param>
    /// <param name="value">مقدار</param>
    /// <returns></returns>
    Task SaveAsync<TSettings>(string keySuffix, TSettings value) where TSettings : ISettings<TSettings>, new();

    /// <summary>
    /// ذخیره تنظیمات
    /// </summary>
    /// <typeparam name="TSettings">مدل تنظیمات</typeparam>
    /// <param name="value">مقدار</param>
    /// <returns></returns>
    Task SaveAsync<TSettings>(TSettings value) where TSettings : ISettings<TSettings>, new();
}
