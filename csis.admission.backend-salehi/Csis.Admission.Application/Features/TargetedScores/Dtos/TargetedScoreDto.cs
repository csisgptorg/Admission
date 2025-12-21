using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.TargetedScores.Dtos;

/// <summary>
/// هدفمندی
/// </summary>
public record TargetedScoreDto : BaseDto<TargetedScoreDto, TargetedScore>
{
    /// <summary>
    /// دسته بندی شده
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// IsOk
    /// </summary>
    public bool? IsOk { get; set; }

    /// <summary>توضیحات</summary>
    public string Description { get; set; }
}
