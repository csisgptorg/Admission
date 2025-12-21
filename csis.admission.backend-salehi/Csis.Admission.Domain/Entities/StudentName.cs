using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class StudentName : BaseEntity
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string LastName { get; set; }
}
