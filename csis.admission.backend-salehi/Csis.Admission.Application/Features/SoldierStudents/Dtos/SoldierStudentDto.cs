using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.SoldierStudents.Dtos;

/// <inheritdoc/>
public sealed record SoldierStudentDto : BaseDto<SoldierStudentDto, SoldierStudent>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string StartDate { get; set; }

    /// <inheritdoc/>
    public string EndDate { get; set; }

    /// <inheritdoc/>
    public string Place { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<SoldierStudent, SoldierStudentDto> mapping) {
        mapping.ForMember(dto => dto.StartDate, config => config.MapFrom(model => model.StartDate.IntDateToString()));
        mapping.ForMember(dto => dto.EndDate, config => config.MapFrom(model => model.EndDate.IntDateToString()));
    }
}
