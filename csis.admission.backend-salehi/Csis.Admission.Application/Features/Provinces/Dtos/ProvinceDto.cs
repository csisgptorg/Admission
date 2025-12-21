using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Provinces.Dtos;

/// <summary>استان</summary>
public sealed record ProvinceDto : BaseDto<ProvinceDto, Province,short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
