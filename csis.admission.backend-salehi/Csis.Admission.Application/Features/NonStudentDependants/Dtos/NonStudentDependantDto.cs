using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Application.Features.NonStudentDependants.Dtos;

/// <summary>
/// مدل نمایشی موجودیت تکفل های غیرطلبه
/// </summary>
public sealed record NonStudentDependantDto : BaseDto<NonStudentDependantDto, NonStudentDependant>
{
    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// شناسه غیر طلبه
    /// </summary>
    public long NonStudentCodm { get; init; }

    /// <summary>
    /// فعال بودن
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// نسبت
    /// </summary>
    public DependentRelation Relationship { get; init; }

    /// <summary>
    /// شعبه
    /// </summary>
    public byte? Branch { get; init; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public DateOnly CaseCreateDate { get; init; }

    /// <summary>
    /// تاریخ غیرفعال سازی پرونده
    /// </summary>
    public DateOnly? CaseDeactiveDate { get; init; }

    /// <summary>
    /// ترتیب نسبت
    /// برای والدین صفر است
    /// </summary>
    public byte RelationshipOrder { get; init; }

    /// <summary>
    /// دلیل وضعیت فعال یا غیرفعالی
    /// </summary>
    public byte? StatusReason { get; init; }
}
