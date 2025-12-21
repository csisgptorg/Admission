using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>بارداری</summary>
public class Pregnancy : SoftDeletedBaseEntity, IAuditable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public int StartDate { get; set; }

    /// <inheritdoc/>
    public int EndDate { get; set; }

    /// <inheritdoc/>
    public long DocumentId { get; set; }

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
