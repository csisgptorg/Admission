using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.StudentDependents.Dtos;

/// <summary>
/// نمایش اطلاعات سرپرست و اعضای خانواده
/// </summary>
public sealed record FamilyInfoDto : BaseDto<FamilyInfoDto, DependentSummary, long>
{
    /// <inheritdoc/>
    public string FirstName { get; init; }

    /// <summary> </summary>
    public string LastName { get; init; }

    /// <inheritdoc/>
    public string FullName { get; init; }

    /// <inheritdoc/>
    public DependentRelation? RelationId { get; init; }

    /// <inheritdoc/>
    public string RelationTitle { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }

    /// <inheritdoc/>
    public bool IsMarried { get; init; }

    /// <summary> تاریخ ازدواج </summary>
    public string? MarriageDate { get; init; }

    /// <inheritdoc/>
    public string NationalCode { get; init; }

    /// <inheritdoc/>
    public string BirthDate { get; init; }


    public override void CustomMappings(IMappingExpression<DependentSummary, FamilyInfoDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        mapping.ForMember(dest => dest.RelationTitle, opt => opt.MapFrom(src => src.Relation.GetEnumDisplayName()));
        mapping.ForMember(dest => dest.RelationId, opt => opt.MapFrom(src => src.Relation));
        mapping.ForMember(dest => dest.DependentId, opt => opt.MapFrom(src => src.Id));
        mapping.ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode));
        mapping.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Value.ToPersianDateTime()));
        mapping.ForMember(dest => dest.MarriageDate, opt => opt.MapFrom(src => src.MarriageDate.Value.ToPersianDateTime()));
    }
}
