using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Rurals.Dtos;

/// <summary>
/// دهستان
/// </summary>
public sealed record RuralDto : BaseDto<RuralDto, Rural, short>
{
    /// <summary>
    /// بخش
    /// </summary>
    public short PortionId { get; set; }

    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
