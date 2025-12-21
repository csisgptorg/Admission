using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>مدل غیر فعال کردن تکفل</summary>
public class DeActiveDependentV4Model(int codm, long dependentId, DependentDeActiveReasonEnum deActiveReason) : RepoCommandLogParam
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; }=codm;

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; }=dependentId;

    /// <summary>علت</summary>
    public DependentDeActiveReasonEnum DeActiveReason { get; }=deActiveReason;
}
