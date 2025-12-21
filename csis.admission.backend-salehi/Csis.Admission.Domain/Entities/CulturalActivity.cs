using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// فعالیت های فرهنگی
/// </summary>
public class CulturalActivity : SoftDeletedBaseEntity, IFilterable, IAuditable
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نوع مدیریت فرهنگی
    /// </summary>
    public CulturalKind? Kind { get; set; }

    /// <summary>
    /// سایر انواع
    /// </summary>
    public string? OtherKind { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public int? Year { get; set; }

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

    /// <summary>
    /// GetFilterableFields
    /// </summary>
    /// <returns></returns>
    public string[] GetFilterableFields() {
        return [nameof(Codm)];
    }
}
