using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.PictureHistories.Dtos;
/// <summary>
/// سابقه تصاویر پرسنلی
/// </summary>
public sealed record PictureHistoryDto : BaseDto<PictureHistoryDto, PictureHistory>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// Picture
    /// </summary>
    public string Picture { get; set; }

    /// <summary>
    /// DateCreated
    /// </summary>
    public string DateCreated { get; set; }

    /// <summary>
    /// UserId
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<PictureHistory, PictureHistoryDto> mapping) {
        mapping.ForMember(dto => dto.DateCreated, config => config.MapFrom(model => model.DateCreated.IntDateToString()));
        mapping.ForMember(dto => dto.Picture, config => config.MapFrom(model => model.Picture.ByteToBase64String()));
    }
}
