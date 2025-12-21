using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

public class EmployeeIdentification : SoftDeletedBaseEntity
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نام محل کار
    /// </summary>
    public string EmployeeName { get; set; }

    /// <summary>
    /// شناسه پرسنلی
    /// </summary>
    public int PersonnelId { get; set; }

    /// <summary>
    /// شناسه هشدار اشتغال
    /// </summary>
    public int EshteghalWarningId { get; set; }

    /// <summary>
    /// آیا فرآیند تکمیل شده است؟
    /// </summary>
    public bool IsFinish { get; set; }
}
