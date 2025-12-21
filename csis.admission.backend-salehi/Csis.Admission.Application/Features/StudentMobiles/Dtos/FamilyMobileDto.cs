namespace Csis.Admission.Application.Features.StudentMobiles.Dtos;

/// <inheritdoc/>
public sealed record FamilyMobileDto
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long? DependentId { get; set; }

    /// <inheritdoc/>
    public string NationalCode { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string LastName { get; set; }

    /// <inheritdoc/>
    public string Mobile { get; set; }
}
