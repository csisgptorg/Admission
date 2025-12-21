/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// ساختار پروفایل گزارش
/// </summary>
/// <param name="Selects">آیتم های نمایشی</param>
/// <param name="Sorts">مرتب سازی</param>
/// <param name="ReceptionConditions">شرط های پذیرشی</param>
/// <param name="ApplicationConditions">شرط های بیزینس</param>
public sealed record ReportProfileStructure(string[] Selects, ReportProfileSort[] Sorts, ReportProfileCondition ReceptionConditions, ReportProfileCondition ApplicationConditions);

/// <summary>
/// مرتب سازی
/// </summary>
/// <param name="Name">نام فیلد</param>
/// <param name="Direction">جهت</param>
public sealed record ReportProfileSort(string Name, int Direction);

/// <summary>
/// شرط گزارش
/// </summary>
/// <param name="Name">نام فیلد</param>
/// <param name="Operator">عملگر</param>
/// <param name="Value">مقدار شرط</param>
/// <param name="Values">مقدار شرط چندمقداری</param>
/// <param name="Conjunction">AND - OR</param>
/// <param name="Filters">فیلترها</param>
public sealed record ReportProfileCondition(string Name, int? Operator, object Value, object Values, string Conjunction, List<ReportProfileCondition> Filters);
