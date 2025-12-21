using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ExcellentEducationYears.Dtos;

/// <summary>سال تحصیلی ممتازین</summary>
public sealed record ExcellentEducationYearDto : BaseDto<ExcellentEducationYearDto, ExcellentEducationYear, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
