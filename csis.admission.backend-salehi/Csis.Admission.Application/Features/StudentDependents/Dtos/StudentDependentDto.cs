using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.StudentDependents.Dtos;

/// <summary>
/// تکفل های طلبه
/// </summary>
public sealed record StudentDependentDto : BaseDto<StudentDependentDto, DependentSummary, long>
{
    /// <summary>
    /// کد مرکز سرپرست
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// شناسه تکفل
    /// </summary>
    public long? DependentId { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// وضعیت تأهل
    /// </summary>
    public bool IsMarried { get; init; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public string? MarriageDate { get; init; }

    /// <summary>
    /// فعال بودن پرونده
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// وضعیت فوت
    /// </summary>
    public bool IsDead { get; init; }

    public override void CustomMappings(IMappingExpression<DependentSummary, StudentDependentDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(dest => dest.MarriageDate,
            opt => opt.MapFrom(src => src.MarriageDate.HasValue ? src.MarriageDate.Value.IntDateToString() : null));
    }
}

/// <summary>
/// اطلاعات طلبه + تکفل ها
/// </summary>
public sealed record StudentWithDependentsDto
{
    /// <summary>
    /// اطلاعات طلبه
    /// </summary>
    public StudentDependentDto Student { get; init; }
    /// <summary>
    /// لیست تکفل ها
    /// </summary>
    public List<StudentDependentDto> Dependents { get; init; }
}
