using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>کمسیون</summary>
public class CommissionInfo : BaseEntity
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>طلبه</summary>
    public string Student { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <summary>تکفل</summary>
    public string Dependent { get; set; }

    /// <summary>نسبت</summary>
    public DependentRelation Relation { get; set; }

    /// <summary>وضعیت درخواست</summary>
    public string RequestStatus { get; set; }

    /// <summary>نوع</summary>
    public string Type { get; set; }

    /// <summary>تاریخ ساخت</summary>
    public int? CreateDate { get; set; }

    /// <summary></summary>
    public string CreatorFullName { get; set; }
}
