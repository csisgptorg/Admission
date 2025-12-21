using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// ویرایش وضعیت فعال بودن تکفل در پرونده پذیرش
/// </summary>
public class UpdateStudentDependentCaseActiveStatusPrc : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }
    /// <summary>دلیل فعال بودن تکفل</summary>
    public DependentActiveReasonEnum ActiveReason { get; set; }
}
