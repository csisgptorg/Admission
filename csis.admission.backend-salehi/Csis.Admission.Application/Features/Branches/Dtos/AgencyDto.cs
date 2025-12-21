using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Branches.Dtos;

/// <summary>نمایندگی</summary>
public sealed record AgencyDto : BaseDto<AgencyDto, Agency, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; init; }
}
