using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Portions.Dtos;

/// <summary>استان</summary>
public sealed record PortionDto : BaseDto<PortionDto, Portion, short>
{
    /// <summary>شهر</summary>
    public short? CityId { get; set; }

    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
