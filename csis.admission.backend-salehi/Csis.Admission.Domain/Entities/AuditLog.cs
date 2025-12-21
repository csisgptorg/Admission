using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت لاگ تغیرات دیتا
/// </summary>
public sealed class AuditLog : BaseEntity, IFilterable
{
    /// <summary>جدول</summary>
    public string Table { get; set; }

    /// <summary>ستون</summary>
    public string Column { get; set; }

    /// <summary>
    /// نوع عملیات
    /// </summary>
    public AuditActionType ActionType { get; set; }

    /// <summary>
    /// شناسه رکورد جدول
    /// </summary>
    public long? TableRecordId { get; set; }

    /// <summary>
    /// مقدار قبلی
    /// </summary>
    public string OldValue { get; set; }

    /// <summary>
    /// مقدار جدید
    /// </summary>
    public string NewValue { get; set; }

    /// <summary>
    /// مقدار نمایشی قبلی
    /// </summary>
    public string OldValueDisplay { get; set; }

    /// <summary>
    /// مقدار نمایشی جدید
    /// </summary>
    public string NewValueDisplay { get; set; }

    /// <summary>
    /// تاریخ
    /// </summary>
    public int Date { get; set; }

    /// <summary>
    /// ساعت
    /// </summary>
    public TimeOnly Time { get; set; }

    /// <summary>
    /// منبع ایجاد
    /// </summary>
    public DataSource? DataSource { get; set; }
    //enum

    /// <summary>
    /// شناسه درخواست
    /// </summary>
    public int? RequestId { get; set; }

    /// <summary>
    /// شناسه عملیات لاگ
    /// برای تمام لاگ هایی که در یک عملیات ذخیره شده اند یکسان است
    /// </summary>
    public Guid? AuditOperationId { get; set; }

    /// <summary>
    /// شناسه گروه لاگ
    /// برای تمام لاگ های مربوط به یک موجودیت یکسان است
    /// </summary>
    public Guid? AuditGroupId { get; set; }

    /// <summary>
    /// شناسه موقت لاگ برای رکوردهای جدید
    /// </summary>
    public Guid? TempId { get; set; }

    /// <inheritdoc/>
    public int? ApplicationId { get; set; }
    
    /// <summary>شناسه پرسنل</summary>
    public int? PersonnelId { get; set; }

    /// <summary>کد مرکز خدمات</summary>
    public int? Codm { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [];
    }
}
