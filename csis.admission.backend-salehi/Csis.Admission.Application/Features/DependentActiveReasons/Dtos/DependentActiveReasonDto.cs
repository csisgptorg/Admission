using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.DependentActiveReasons.Dtos;

/// <summary>
/// مدل نمایشی دلیل رفع انسداد پرونده
/// </summary>
public sealed record DependentActiveReasonDto : BaseDto<DependentActiveReasonDto, DependentActiveReason,short>
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
