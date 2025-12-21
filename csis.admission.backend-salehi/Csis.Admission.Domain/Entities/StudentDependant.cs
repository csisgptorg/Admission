using Csis.Admission.Domain.Common;
using Csis.Utilities.Annotations;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت تکفل های طلبه
/// </summary>
public sealed class StudentDependant : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// شناسه سرپرست
    /// </summary>
    public int StudentCodm { get; set; }

    /// <summary>
    /// شناسه کاربر
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// نسبت ها
    /// </summary>
    public short Relationship { get; set; }

    /// <summary>
    /// ترتیب نسبت ها
    /// </summary>
    public short RelationshipOrder { get; set; }

    /// <summary>
    /// وضعیت پرونده
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// علت
    /// </summary>
    public byte? StatusReason { get; set; }

    /// <summary>
    /// تاریخ بستن پرونده
    /// </summary>
    public int? CaseExpireDate { get; set; }

    /// <summary>
    /// علت بستن پرونده
    /// </summary>
    public byte? CaseExpireReason { get; set; }

    /// <summary>
    /// شعبه
    /// </summary>
    public short? Branch { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? CommissionRequestId { get; set; }

    /// <summary>
    /// آیا در ایران زندگی میکند
    /// </summary>
    public bool? LiveInIran { get; set; }

    /// <summary>
    /// کد اصلی
    /// </summary>
    public int? StudentFileCodm { get; set; }

    /// <summary>
    /// کد انتقال
    /// </summary>
    public int? CaseTransferedTo { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    public int? CaseCreateDate { get; set; }

    /// <summary>
    /// تاریخ انسداد
    /// </summary>
    public int? CaseDeactiveDate { get; set; }

    /// <summary>
    /// صرفا عضو خانواده باید غیر فعال باشد
    /// </summary>
    public bool? ForceDeactivate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? OldCodm { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsDelete { get; set; }

    /// <summary>
    /// کاربر
    /// </summary>
    public Person Person { get; set; }

    /// <summary>
    /// طلبه
    /// </summary>
    [ForeignKey(nameof(StudentCodm))]
    public Student StudentCodmNavigation { get; private set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [];
    }
}
