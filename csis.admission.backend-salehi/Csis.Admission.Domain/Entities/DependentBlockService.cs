using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>خدمات مسدود تکفل</summary>
public sealed class DependentBlockService : SoftDeletedBaseEntity
{
    /// <summary>شناسه مرکز</summary>
    public long DependentId { get; set; }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>تاریخ</summary>
    public int BlockDate { get; init; }

    /// <summary>علت</summary>
    public string Reason { get; set; }

    /// <summary>شناسه خدمت</summary>
    public int ServiceId { get; set; }

    /// <summary>خدمت</summary>
    public CsisService Service { get; set; }

    /// <summary>تکفل</summary>
    public DependentSummary Dependent { get; set; }
}
