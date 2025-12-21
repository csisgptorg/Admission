/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت فایل آپلود شده در سامانه
/// </summary>
public sealed class UploadedFile : BaseEntity
{
    /// <summary>
    /// شناسه فایل در سامانه مدیریت فایل
    /// </summary>
    public Guid FileIdentifier { get; init; }

    /// <summary>
    /// نوع فایل
    /// </summary>
    public FileTypes Type { get; init; }
}
