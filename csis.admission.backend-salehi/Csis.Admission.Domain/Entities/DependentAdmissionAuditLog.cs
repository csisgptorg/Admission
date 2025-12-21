using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>سوابق اطلاعات پذیریش تکفل</summary>
public class DependentAdmissionAuditLog : SoftDeletedBaseEntity
{
    /// <summary>نام کامل کاربر</summary>
    public string UserFullName { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }

    /// <summary>طلبه</summary>
    public string DependentFullName { get; set; }

    /// <summary>نسبت تکفل</summary>
    public DependentRelation Relation { get; set; }

    /// <summary>شناسه جدول</summary>
    public int TableId { get; set; }

    /// <summary>جدول</summary>
    public string TableTitle { get; set; }

    /// <summary>شناسه ستون</summary>
    public int FieldId { get; set; }

    /// <summary>ستون</summary>
    public string FieldTitle { get; set; }

    /// <summary>شناسه رکورد جدول</summary>
    public int? TableRecordId { get; set; }

    /// <summary>مقدار قدیم</summary>
    public string OldValueDisplay { get; set; }

    /// <summary>مقدار جدید</summary>
    public string NewValueDisplay { get; set; }

    /// <summary>تاریخ</summary>
    public int? Date { get; set; }

    /// <summary>ساعت</summary>
    public TimeSpan Time { get; set; }

    /// <summary>محل ثبت اطلاعات</summary>
    public string DataSourceTitle { get; set; }

    /// <summary>شناسه درخواست</summary>
    public string RequestId { get; set; }
}
