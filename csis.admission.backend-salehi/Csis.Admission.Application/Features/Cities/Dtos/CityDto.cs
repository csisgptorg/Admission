using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Cities.Dtos;

/// <summary>شهر</summary>
public sealed record CityDto : BaseDto<CityDto, City, short>
{
    /// <summary>استان</summary>
    public short ProvinceId { get; set; }

    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
