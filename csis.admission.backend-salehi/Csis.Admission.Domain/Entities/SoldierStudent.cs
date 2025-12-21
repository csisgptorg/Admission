using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>سرباز طلبه</summary>
public class SoldierStudent : SoftDeletedBaseEntity,IAuditable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string Place { get; set; }

    /// <inheritdoc/>
    public int StartDate { get; set; }

    /// <inheritdoc/>
    public int EndDate { get; set; }

    /// <summary>شناسه موقت</summary>
    public Guid? TempId { get; set; }

    /// <summary>منبع دیتا</summary>
    public DataSource? AuditDataSource { get; set; }
    /// <summary>شناسه درخواست</summary>
    public int? AuditRequestId { get; set; }
    /// <summary>شناسه شخص</summary>
    public int? AuditPersonId { get; set; }
}
