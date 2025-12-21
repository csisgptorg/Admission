using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Common.Interfaces;

/// <inheritdoc/>
public interface ICsisHealthInsuranceService
{
    /// <inheritdoc/>
    Task<CurrentHealthInsuranceCaseStateDto> CaseState(int codm,long? dependentId, CancellationToken cancellation);
}
