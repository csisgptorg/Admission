using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// مدل درخواست ثبت طلاق
/// </summary>
public class SetDependentDivorceModel : RepoCommandLogParam
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// شناسه همسر
    /// </summary>
    public long DependentId { get; set; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public int DivorceDate { get; set; }
}
