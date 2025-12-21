/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// مدل نمایشی دارای اطلاعات خلاصه کارمند
/// </summary>
public interface IEmployeeAbstractInfoDto
{
    /// <summary>
    /// کد پرسنلی
    /// </summary>
    int PersonnelId { get; set; }

    /// <summary>
    /// شناسه تکفل
    /// </summary>
    int? TakafolId { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// نسبت
    /// </summary>
    string Relation { get; set; }
}
