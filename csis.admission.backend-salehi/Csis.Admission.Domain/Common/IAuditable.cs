using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Common;

/// <summary>
/// موجودیت دارای لاگ خودکار
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// شناسه موقت لاگ برای رکوردهای جدید
    /// </summary>
    public Guid? TempId { get; set; }

    /// <summary>
    /// منبع آخرین تغییر در رکورد
    /// </summary>
    public DataSource? AuditDataSource { get; set; }

    /// <summary>
    /// شناسه آخرین درخواستی که باعث تغییر در رکورد شده است
    /// </summary>
    public int? AuditRequestId { get; set; }

    /// <summary>
    /// شناسه فردی که رکورد متعلق به او می‌باشد
    /// </summary>
    public int? AuditPersonId { get; set; }
}
