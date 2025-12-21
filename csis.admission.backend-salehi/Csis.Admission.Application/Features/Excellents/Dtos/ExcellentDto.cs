using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Excellents.Dtos;

/// <summary>
/// ممتازین
/// </summary>
public sealed record ExcellentDto : BaseDto<ExcellentDto, Excellent>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public short? EducationYearId { get; set; }

    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public string EducationYear { get; set; }

    /// <summary>
    /// آیدی مقطع تحصیلی
    /// </summary>
    public short? EducationLevelId { get; set; }

    /// <summary>
    /// آیدی مقطع تحصیلی
    /// </summary>
    public string EducationLevel { get; set; }

    /// <summary>
    /// معدل
    /// </summary>
    public double? Average { get; set; }

    /// <summary>توضیحات</summary>
    public string Description { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<Excellent, ExcellentDto> mapping) {
        mapping.ForMember(dto => dto.EducationYear, config => config.MapFrom(model => model.EducationYear.Title));
        mapping.ForMember(dto => dto.EducationLevel, config => config.MapFrom(model => model.EducationLevel.Title));
    }
}
