using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// رتبه پژوهش
/// </summary>
public class ResearchGrade : SoftDeletedBaseEntity, IFilterable, IAuditable
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter ApprovalCenter { get; set; }

    /// <summary>
    /// رتبه
    /// </summary>
    public short Grade { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    public int RegisterDate { get; set; }

    /// <summary>
    /// تاریخ اعتبار
    /// </summary>
    public int? ExpirationDate { get; set; }

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

    /// <inheritdoc/>>
    public string[] GetFilterableFields() {
        return [nameof(Codm)];
    }
}
