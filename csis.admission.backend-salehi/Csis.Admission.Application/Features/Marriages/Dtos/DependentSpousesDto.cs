
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Marriages.Dtos;

/// <summary>
/// مشخصات همسر - خانم ها
/// </summary>
public sealed record DependentSpousesDto : BaseDto<DependentSpousesDto, DependentSummary, long>
{
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }


    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public string? BirthDate { get; init; }

    public override void CustomMappings(IMappingExpression<DependentSummary, DependentSpousesDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(x => x.BirthDate, opt => opt.MapFrom(x => x.BirthDate.Value.ToPersianDateTime()));
    }
}
