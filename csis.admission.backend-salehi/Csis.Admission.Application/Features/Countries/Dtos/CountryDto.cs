using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Countries.Dtos;

/// <summary>شهر</summary>
public sealed record CountryDto : BaseDto<CountryDto, Country, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
