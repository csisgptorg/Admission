using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>خدمات مسدود</summary>
public sealed class StudentBlockService : SoftDeletedBaseEntity
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>تاریخ</summary>
    public int BlockDate { get; set; }

    /// <summary>علت</summary>
    public string Reason { get; set; }

    /// <summary>شناسه خدمت</summary>
    public int ServiceId { get; set; }

    /// <summary>خدمت</summary>
    public CsisService Service { get; set; }
}
