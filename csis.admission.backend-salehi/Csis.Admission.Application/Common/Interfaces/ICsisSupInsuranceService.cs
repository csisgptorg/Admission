using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Common.Interfaces;

/// <inheritdoc/>
public interface ICsisSupInsuranceService
{
    /// <inheritdoc/>
    Task<CurrentSupInsuranceCaseStateDto> GetHealthStatus(int codm, long? dependentId, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<CurrentSupInsuranceCaseStateDto> GetLifeStatus(int codm, long? dependentId, CancellationToken cancellation);
}

