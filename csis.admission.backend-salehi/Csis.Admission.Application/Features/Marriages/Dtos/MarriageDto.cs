using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.Marriages.Dtos;

/// <summary>
/// مدل نمایشی موجودیت ازدواج
/// </summary>
public sealed record MarriageDto : BaseDto<MarriageDto, Marriage>
{
    /// <summary>
    /// شناسه شوهر
    /// </summary>
    public int? HusbandPersonId { get; init; }

    /// <summary>
    /// شناسه همسر
    /// </summary>
    public int? WifePersonId { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public DateOnly? DeathDate { get; init; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public DateOnly? DivorceDate { get; init; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public DateOnly? MarriageDate { get; init; }
}
