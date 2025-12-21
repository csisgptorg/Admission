using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class Otp : SoftDeletedBaseEntity<long>, IFilterable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long? DependentId { get; set; }

    /// <inheritdoc/>
    public string Mobile { get; set; }

    /// <inheritdoc/>
    public int Code { get; set; }

    /// <inheritdoc/>
    public string JsonPayload { get; set; }

    /// <inheritdoc/>
    public OtpType Type { get; set; }

    /// <inheritdoc/>
    public bool Used { get; set; } = false;

    /// <inheritdoc/>
    public DateTime ExpirationDate { get; set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [nameof(Codm), nameof(Code), nameof(Type), nameof(Used), nameof(ExpirationDate)];
    }
}
