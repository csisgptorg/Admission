using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.EliteLevels.Dtos;

/// <summary>سطح نخبگانی</summary>
public sealed record EliteLevelDto : BaseDto<EliteLevelDto, EliteLevel, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
