using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Towns.Dtos;

/// <summary>
/// شهرستان
/// </summary>
public sealed record TownDto : BaseDto<TownDto, Town, short>
{
    /// <summary>
    /// بخش
    /// </summary>
    public short PortionId { get; set; }

    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
