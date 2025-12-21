namespace Csis.Admission.Domain.Enums;

/// <summary>
/// Audit log type
/// </summary>
public enum AuditActionType : short
{
    /// <summary>
    /// Create record
    /// </summary>
    Create = 1,

    /// <summary>
    /// Update record
    /// </summary>
    Update = 2,

    /// <summary>
    /// Delete record
    /// </summary>
    Delete = 3
}
