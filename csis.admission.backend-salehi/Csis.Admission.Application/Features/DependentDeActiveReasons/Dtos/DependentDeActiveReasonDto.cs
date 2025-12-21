using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.DependentDeActiveReasons.Dtos;

/// <summary>
/// مدل نمایشی دلیل انسداد پرونده
/// </summary>
public sealed record DependentDeActiveReasonDto : BaseDto<DependentDeActiveReasonDto, DependentDeActiveReason, short>
{
    /// <summary>
    /// عنوان
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// نوع
    /// </summary>
    public string Type { get; init; }
}
