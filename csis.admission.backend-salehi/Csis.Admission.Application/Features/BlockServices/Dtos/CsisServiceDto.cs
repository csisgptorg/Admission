using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Dtos;

/// <summary>دریافت</summary>
public sealed record CsisServiceDto : BaseDto<CsisServiceDto, CsisService>
{
    /// <summary>عنوان</summary>
    public string Title { get; init; }
}
