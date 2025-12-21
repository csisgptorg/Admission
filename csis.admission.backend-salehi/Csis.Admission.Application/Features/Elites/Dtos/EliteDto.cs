using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Elites.Dtos;

/// <summary>
/// نخبگان
/// </summary>
public sealed record EliteDto : BaseDto<EliteDto, Elite>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// آیدی نوع نخبگی
    /// </summary>
    public short? EliteTypeId { get; set; }

    /// <summary>
    /// نوع نخبگی
    /// </summary>
    public string EliteType { get; set; }

    /// <summary>
    /// آیدی سطح نخبگی
    /// </summary>
    public short? EliteLevelId { get; set; }

    /// <summary>
    /// سطح نخبگی
    /// </summary>
    public string EliteLevel { get; set; }

    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public string EndDate { get; set; }

    /// <summary>
    /// مرجع
    /// </summary>
    public string ApprovalCenterTitle { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Elite, EliteDto> mapping) {
        mapping.ForMember(dto => dto.EliteLevel, config => config.MapFrom(model => model.EliteLevel.Title));
        mapping.ForMember(dto => dto.EliteType, config => config.MapFrom(model => model.EliteType.Title));
        mapping.ForMember(dto => dto.StartDate, config => config.MapFrom(model => model.StartDate.IntDateToString()));
        mapping.ForMember(dto => dto.EndDate, config => config.MapFrom(model => model.EndDate.IntDateToString()));
    }
}
