using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.NonStudents.Dtos;

/// <summary>
/// مدل نمایشی موجودیت غیر طلبه
/// </summary>
public sealed record NonStudentDto : BaseDto<NonStudentDto, NonStudent, long>
{
    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// نمایندگی
    /// </summary>
    public byte? Agency { get; init; }

    /// <summary>
    /// شعبه
    /// </summary>
    public byte? Branch { get; init; }

    /// <summary>
    /// تاریخ مسدودی پرونده
    /// </summary>
    public DateOnly? CaseBlockDate { get; init; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public DateOnly CaseCreateDate { get; init; }

    /// <summary>
    /// تاریخ انقضا پرونده
    /// </summary>
    public DateOnly? CaseExpireDate { get; init; }

    /// <summary>
    /// وضعیت
    /// </summary>
    public NonStudentStatus Status { get; init; }

    /// <summary>
    /// نوع غیر طلبه
    /// </summary>
    public NonStudentType? Type { get; init; }
}
