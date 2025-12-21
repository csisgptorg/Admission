/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت تنظیمات
/// </summary>
public sealed class Setting : BaseEntity
{
    /// <summary>
    /// کلید تنظیمات
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// نسخه تنظیمات
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// مقدار به صورت JSON
    /// </summary>
    public string Value { get; set; }
}
