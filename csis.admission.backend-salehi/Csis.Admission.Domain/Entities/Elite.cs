using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;
/// <summary>
/// نخبگان
/// </summary>
public class Elite : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// آیدی نوع نخبگی
    /// </summary>
    public short? EliteTypeId { get; set; }

    /// <summary>
    /// نوع نخبگی
    /// </summary>
    public EliteType EliteType { get; set; }

    /// <summary>
    /// آیدی سطح نخبگی
    /// </summary>
    public short? EliteLevelId { get; set; }

    /// <summary>
    /// سطح نخبگی
    /// </summary>
    public EliteLevel EliteLevel { get; set; }

    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public int? StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public int? EndDate { get; set; }

    /// <summary>
    /// مرجع
    /// </summary>
    public string ApprovalCenterTitle { get; set; }

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
