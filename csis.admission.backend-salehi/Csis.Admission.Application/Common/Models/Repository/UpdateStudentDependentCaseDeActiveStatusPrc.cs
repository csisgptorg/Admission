using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// ویرایش وضعیت غیرفعال بودن تکفل در پرونده پذیرش
/// </summary>
public class UpdateStudentDependentCaseDeActiveStatusPrc : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }
    /// <summary>دلیل فعال بودن تکفل</summary>
    public DependentDeActiveReasonEnum DeActiveReason { get; set; }
}
