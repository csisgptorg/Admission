using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>ایثارگری</summary>
public class Veteran : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تعداد روز دفاع از حرم
    /// </summary>
    public int? HaramDefenceDays { get; set; }

    /// <summary>
    /// تعداد روز دفاع مقدس
    /// </summary>
    public int? HolyDefenseDays { get; set; }

    /// <summary>
    /// تعداد روز آزادگی
    /// </summary>
    public int? CaptivityDays { get; set; }

    /// <summary>
    /// تعداد روز زندان قبل از انقلاب
    /// </summary>
    public int? JailDays { get; set; }

    /// <summary>
    /// تعداد روز تبعید قبل از انقلاب
    /// </summary>
    public int? ExileDays { get; set; }

    /// <summary>
    /// در صد جانبازی
    /// </summary>
    public short? VeteranPercent { get; set; }

    /// <summary>
    ///نسبت با شهید
    /// </summary>
    public DependentRelation? RelationWithMartyr { get; set; }

    /// <summary>
    ///نوع شهادت
    /// </summary>
    public MartyrType? MartyrType { get; set; }

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
