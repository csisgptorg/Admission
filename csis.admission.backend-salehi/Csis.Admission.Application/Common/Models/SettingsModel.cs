/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// مدل تنظیمات
/// </summary>
/// <typeparam name="TSettings">نوع تنظیمات</typeparam>
/// <param name="Value">مقدار تنظیمات</param>
/// <param name="Version">نسخه</param>
public sealed record SettingsModel<TSettings>(TSettings Value, int Version) where TSettings : new();
