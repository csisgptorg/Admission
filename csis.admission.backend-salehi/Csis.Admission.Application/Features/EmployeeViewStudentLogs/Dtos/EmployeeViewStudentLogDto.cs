using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ViewLogs.Dtos;

/// <summary>لاگ مشاهده اطلاعات طلبه توسط کارمند</summary>
public sealed record EmployeeViewStudentLogDto : BaseDto<EmployeeViewStudentLogDto, EmployeeViewStudentLog, long>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// PersonnelId
    /// </summary>
    public int PersonnelId { get; init; }

    /// <summary>
    /// Date
    /// </summary>
    public string Date { get; init; }

    /// <summary>
    /// Time
    /// </summary>
    public string Time { get; init; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<EmployeeViewStudentLog, EmployeeViewStudentLogDto> mapping) {
        mapping.ForMember(dto => dto.Date, config => config.MapFrom(model => model.Date.IntDateToString()));
        mapping.ForMember(dto => dto.Time, config => config.MapFrom(model => model.Time.TimeToString()));
    }
}
