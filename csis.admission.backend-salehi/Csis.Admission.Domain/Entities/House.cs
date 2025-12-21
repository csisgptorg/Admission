using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// مسکن
/// </summary>
public class House : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// وضعیت سکونت (شخصی، حمایتی، اجاره‌ای/رهنی)
    /// </summary>
    public HouseStatus HouseStatus { get; set; }

    /// <summary>
    /// جزئیات وضعیت سکونت (سازمانی، پدری، منزل همسر، سایر)
    /// </summary>
    public HouseStatusItem? HouseStatusItem { get; set; }

    /// <summary>
    /// توضیح جزئیات وضعیت سکونت (وقتی سایر انتخاب شود)
    /// </summary>
    public string? HouseStatusItemDesc { get; set; }

    /// <summary>
    /// آیا دارای خانه شخصی می‌باشید؟
    /// </summary>
    public bool? HasHouse { get; set; }

    /// <summary>
    /// آیا دارای زمین شخصی می‌باشید؟
    /// </summary>
    public bool? HasLand { get; set; }

    /// <summary>
    /// آیا در حجره یا خوابگاه نیز سکونت دارید؟
    /// </summary>
    public bool? LiveInCell { get; set; }

    /// <inheritdoc/>
    public Request Request { get; set; }

    /// <inheritdoc/>
    public long? RequestId { get; set; }
    /// <summary>
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
