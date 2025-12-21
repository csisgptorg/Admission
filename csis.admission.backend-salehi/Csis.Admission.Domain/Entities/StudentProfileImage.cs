using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class StudentProfileImage : BaseEntity
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public Gender Gender { get; set; }

    /// <inheritdoc/>
    public byte[] Image { get; set; }

    /// <inheritdoc/>
    public int? DateCreated { get; set; }

    /// <inheritdoc/>
    public int? UserId { get; set; }
}
