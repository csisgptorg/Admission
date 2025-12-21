using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت تکفل های غیرطلبه
/// </summary>
public sealed class NonStudentDependant : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// شناسه غیر طلبه
    /// </summary>
    public long NonStudentCodm { get; set; }

    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// نسبت
    /// </summary>
    public DependentRelation Relationship { get; set; }    

    /// <summary>
    /// ترتیب نسبت
    /// برای والدین صفر است
    /// </summary>
    public byte RelationshipOrder { get; set; }

    /// <summary>
    /// فعال بودن
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// دلیل وضعیت فعال یا غیرفعالی
    /// </summary>
    public byte? StatusReason { get; set; }
    //enum

    /// <summary>
    /// شعبه
    /// </summary>
    public byte? Branch { get; set; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public DateOnly CaseCreateDate { get; set; }

    /// <summary>
    /// تاریخ غیرفعال سازی پرونده
    /// </summary>
    public DateOnly? CaseDeactiveDate { get; set; }

    /// <summary>
    /// سرپرست
    /// </summary>
    public NonStudent Householder { get; private set; }

    /// <summary>
    /// شخس
    /// </summary>
    public Person Person { get; private set; }

    /// <summary>
    /// اسناد
    /// </summary>
    public List<RequestDocument> Documents { get; private set; } = [];

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [];
    }
}
