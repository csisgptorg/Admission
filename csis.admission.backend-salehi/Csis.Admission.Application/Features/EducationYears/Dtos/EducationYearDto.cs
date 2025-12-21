using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.EducationYears.Dtos;

/// <summary>سال تحصیلی</summary>
public sealed record EducationYearDto : BaseDto<EducationYearDto, EducationYear, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
