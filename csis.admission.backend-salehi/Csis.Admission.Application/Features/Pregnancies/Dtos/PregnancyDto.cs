using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Pregnancies.Dtos;
/// <summary>
/// بارداری
/// </summary>
public sealed record PregnancyDto : BaseDto<PregnancyDto, Pregnancy>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// StartDate
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// EndDate
    /// </summary>
    public string EndDate { get; set; }

    /// <summary>
    /// کد رهگیری سامانه سخا
    /// </summary>
    public int RequestId { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<Pregnancy, PregnancyDto> mapping) {
        mapping.ForMember(dto => dto.StartDate, config => config.MapFrom(model => model.StartDate.IntDateToString()));
        mapping.ForMember(dto => dto.EndDate, config => config.MapFrom(model => model.EndDate.IntDateToString()));
    }
}
