using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.UniversityEducations.Dtos;

/// <summary>
/// تحصیلات دانشگاهی
/// </summary>
public sealed record StudentUniversityEducationDto : BaseDto<StudentUniversityEducationDto, UniversityEducation>
{
    /// <summary> </summary>
    public int Codm { get; init; }

    /// <summary>در حال تحصیل</summary>
    public bool InStudy { get; init; }

    /// <summary></summary>
    public StudyLevel? StudyLevel { get; init; }

    /// <summary></summary>
    public string StudyLevelTitle => StudyLevel?.GetEnumDisplayName();

    /// <summary></summary>
    public string CourseStudy { get; init; }

    /// <summary> </summary>
    public UniversityTypeEnum? UniversityType { get; init; }

    /// <summary> </summary>
    public string UniversityTypeTitle => UniversityType?.GetEnumDisplayName();

    /// <summary></summary>
    public string UniversityName { get; init; }

    /// <summary></summary>
    public string ProvinceTitle { get; init; }

    /// <summary></summary>
    public string StartDate { get; init; }

    /// <summary></summary>
    public string EndDate { get; init; }

    /// <summary></summary>
    public double? Average { get; init; }

    /// <summary></summary>
    public int? ValidityDate { get; init; }

    /// <summary></summary>
    public string ValidityDateStr => ValidityDate.IntDateToString();

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<UniversityEducation, StudentUniversityEducationDto> mapping) {
        mapping.ForMember(dto => dto.StartDate, config => config.MapFrom(model => model.StartDate.IntDateToString()));
        mapping.ForMember(dto => dto.EndDate, config => config.MapFrom(model => model.EndDate.IntDateToString()));
    }
}
