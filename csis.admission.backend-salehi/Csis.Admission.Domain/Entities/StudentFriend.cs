using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class StudentFriend : SoftDeletedBaseEntity, IAuditable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public int? FriendCodm { get; set; }

    /// <inheritdoc/>
    public long? Mobile { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string LastName { get; set; }

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
