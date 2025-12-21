using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;

/// <summary>
/// همسر های فعال امام جماعت
/// </summary>
public sealed record ImamJamaatDependentCommandDto : BaseCommandDto<ImamJamaatDependentCommandDto, ImamJamaatDependent, long>
{
    /// <summary>
    /// شناسه همسر امام جماعت
    /// </summary>
    public long DependentId { get; set; }
}
