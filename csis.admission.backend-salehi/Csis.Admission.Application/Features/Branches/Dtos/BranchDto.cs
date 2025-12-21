using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Branches.Dtos;

/// <summary>شعبه</summary>
public sealed record BranchDto : BaseDto<BranchDto, Branch, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; init; }

    /// <summary>کد استان</summary>
    public int ProvinceId { get; init; }
}
