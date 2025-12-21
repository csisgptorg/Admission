/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت پروفایل گزارش
/// </summary>
public sealed class ReportProfile : SoftDeletedBaseEntity
{
    /// <summary>
    /// عنوان پروفایل
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// نوع گزارش
    /// </summary>
    public ReportProfileType ReportType { get; init; }

    /// <summary>
    /// نوع پروفایل
    /// </summary>
    public ReportProfileType ProfileType { get; init; }

    /// <summary>
    /// ساختار پروفایل گزارش
    /// </summary>
    public ReportProfileStructure Structure { get; set; }
}
