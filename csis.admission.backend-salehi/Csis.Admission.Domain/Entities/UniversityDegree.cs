using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// UniversityDegree
/// </summary>
public class UniversityDegree : BaseEntity, IAuditable
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    #region AuditLog لاگ خودکار
    /// <inheritdoc/>
    public Guid? TempId { get; set; }

    /// <inheritdoc/>
    public DataSource? AuditDataSource { get; set; }

    /// <inheritdoc/>
    public int? AuditRequestId { get; set; }

    /// <inheritdoc/>
    public int? AuditPersonId { get; set; }
    #endregion
}
