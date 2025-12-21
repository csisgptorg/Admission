/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// مدل نمایشی دارای اطلاعات خلاصه طلبه و تکفل ها
/// </summary>
public interface IStudentAbstractInfoDto
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    string Codm { get; set; }

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

    /// <summary>
    /// کد شعبه
    /// </summary>
    int? BranchId { get; set; }

    /// <summary>
    /// نام شعبه
    /// </summary>
    string BranchName { get; set; }
}
