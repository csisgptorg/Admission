using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.EliteTypes.Dtos;

/// <summary>نوع نخبگان</summary>
public sealed record EliteTypeDto : BaseDto<EliteTypeDto, EliteType, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
