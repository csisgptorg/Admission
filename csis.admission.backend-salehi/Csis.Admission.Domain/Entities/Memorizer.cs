using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>حافظین</summary>
public class Memorizer : SoftDeletedBaseEntity , IAuditable
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <summary>تکفل</summary>
    public DependentSummary Dependent { get; set; }

    /// <summary>
    /// Kind
    /// </summary>
    public MemorizationType? Kind { get; set; }

    /// <summary>
    /// JozCount
    /// </summary>
    public int? JozCount { get; set; }

    /// <summary>
    /// ApprovalCenter
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// CreateDate
    /// </summary>
    public int? CreateDate { get; set; }

    /// <summary>
    /// ExpireDate
    /// </summary>
    public int? ExpireDate { get; set; }

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
