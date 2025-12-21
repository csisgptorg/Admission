using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// مسکن اجاره ای
/// </summary>
public class Tenant : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نام صاحبخانه
    /// </summary>
    public string HostName { get; set; }

    /// <summary>
    /// موبایل صاحب خانه
    /// </summary>
    public string HostMobile { get; set; }

    /// <summary>
    /// مبلغ رهن ریال
    /// </summary>
    public long? MortgageAmount { get; set; }

    /// <summary>
    /// مبلغ اجاره ریال
    /// </summary>
    public long? RentAmount { get; set; }

    /// <summary>
    /// تاریخ شروع قرارداد
    /// </summary>
    public int? StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان قرارداد
    /// </summary>
    public int? EndDate { get; set; }

    /// <summary>
    /// کد رهگیری
    /// </summary>
    public string TrackingCode { get; set; }
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
