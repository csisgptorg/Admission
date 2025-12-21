using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// ثبت طلاق برای طلاب خواهر
/// </summary>
public class SetStudentSisterDivorceModel : RepoCommandLogParam
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public int DivorceDate { get; set; }
}
