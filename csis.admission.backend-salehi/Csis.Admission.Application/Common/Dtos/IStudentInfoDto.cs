/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// مدل نمایشی دارای اطلاعات طلبه و تکفل ها
/// </summary>
public interface IStudentInfoDto
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
    /// شناسه شعبه
    /// </summary>
    int? BranchId { get; set; }

    /// <summary>
    /// نام شعبه
    /// </summary>
    string BranchName { get; set; }

    /// <summary>
    /// نسبت
    /// </summary>
    string Relation { get; set; }

    /// <summary>
    /// کد نسبت درصورتی که تکفل باشد
    /// </summary>
    int? RelationId { get; set; }

    /// <summary>
    /// کد ملی یا کد یکتا
    /// </summary>
    string NationalId { get; set; }

    /// <summary>
    /// ملیت
    /// </summary>
    Nationality Nationality { get; set; }

    /// <summary>
    /// عنوان ملیت
    /// </summary>
    string NationalityTitle { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    Gender Gender { get; set; }

    /// <summary>
    /// عنوان جنسیت
    /// </summary>
    string GenderTitle { get; set; }

    /// <summary>
    /// تلفن همراه
    /// </summary>
    string Mobile { get; set; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public string BirthDate { get; set; }
}
