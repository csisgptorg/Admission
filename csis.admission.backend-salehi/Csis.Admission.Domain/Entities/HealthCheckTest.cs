/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت مورد استفاده جهت بررسی وضعیت سلامت دیتابیس
/// </summary>
public sealed class HealthCheckTest : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string CheckText { get; set; }
}
