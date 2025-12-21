using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت غیر طلبه
/// </summary>
public sealed class NonStudent : SoftDeletedBaseEntity<long>, IFilterable
{
    ///// <summary>
    ///// شناسه طلبه
    ///// </summary>
    //public int Codm { get; set; }

    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// شعبه
    /// </summary>
    public byte? Branch { get; set; }

    /// <summary>
    /// نمایندگی
    /// </summary>
    public byte? Agency { get; set; }

    /// <summary>
    /// وضعیت
    /// </summary>
    public NonStudentStatus Status { get; set; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public DateOnly CaseCreateDate { get; set; }

    /// <summary>
    /// تاریخ انقضا پرونده
    /// </summary>
    public DateOnly? CaseExpireDate { get; set; }

    /// <summary>
    /// تاریخ مسدودی پرونده
    /// </summary>
    public DateOnly? CaseBlockDate { get; set; }

    /// <summary>
    /// نوع غیر طلبه
    /// </summary>
    public NonStudentType? Type { get; set; }

    /// <summary>
    /// کاربر
    /// </summary>
    public Person Person { get; private set; }

    /// <summary>
    /// تکفل ها
    /// </summary>
    public List<NonStudentDependant> NonStudentDependents { get; private set; } = [];

    /// <summary>
    /// اسناد
    /// </summary>
    public List<RequestDocument> Documents { get; private set; } = [];

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [];
    }
}
