using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.CulturalActivities.Dtos;

/// <inheritdoc/>
public sealed record CulturalActivityDto : BaseDto<CulturalActivityDto, CulturalActivity>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نوع مدیریت فرهنگی
    /// </summary>
    public CulturalKind? Kind { get; set; }

    /// <summary>
    /// سایر انواع
    /// </summary>
    public string? OtherKind { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public int? Year { get; set; }
}
