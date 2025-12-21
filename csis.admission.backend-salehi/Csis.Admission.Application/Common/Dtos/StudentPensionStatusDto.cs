namespace Csis.Admission.Application.Common.Dtos;

/// <summary>угЪуб?</summary>
public class StudentPensionStatusDto
{
    /// <inheritdoc/>
    public CsisPensionStatusEnum CsisPensionStatus { get; set; }
    /// <inheritdoc/>
    public bool HasOtherPension { get; set; }
}
