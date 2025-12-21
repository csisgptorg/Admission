using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class Education : BaseEntity, IAuditable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public int? EnteringYear { get; set; }

    /// <inheritdoc/>
    public EducationStatus? EducationStatus { get; set; }

    /// <summary></summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary></summary>
    public long? CaseNumInApprovalCenter { get; set; }

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
