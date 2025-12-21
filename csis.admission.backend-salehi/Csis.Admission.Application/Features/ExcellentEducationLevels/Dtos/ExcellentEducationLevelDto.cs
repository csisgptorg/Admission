using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ExcellentEducationLevels.Dtos;

/// <summary>مقطع ممتازین</summary>
public sealed record ExcellentEducationLevelDto : BaseDto<ExcellentEducationLevelDto, ExcellentEducationLevel, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
