using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>اطلاعات مهم</summary>
public class StudentCase : BaseEntity
{
    /// <summary>
    /// کد مرکز مرتبط با پرونده.
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تاریخ تشکیل پرونده.
    /// </summary>
    public int CaseCreationDate { get; set; }

    /// <summary>
    /// وضعیت فعال یا غیرفعال بودن پرونده.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// تاریخ اعتبار پرونده (در صورت وجود).
    /// </summary>
    public int? CaseValidityDate { get; set; }

    /// <summary>
    /// علت تمدید اعتبار پرونده.
    /// </summary>
    public string ValidityExtensionReasonTitle { get; set; }

    /// <summary>
    /// نشان‌دهنده این که صاحب پرونده طلبه است یا خیر.
    /// </summary>
    public bool IsStudent { get; set; }

    /// <summary>
    /// نشان‌دهنده اینکه پرونده مسدود شده است یا خیر.
    /// </summary>
    public bool IsBlock { get; set; }

    /// <summary>
    /// تاریخ انسداد پرونده (در صورت وجود).
    /// </summary>
    public int? BlockDate { get; set; }

    /// <summary>
    /// علت انسداد پرونده.
    /// </summary>
    public string BlockReasonTitle { get; set; }

    /// <summary>
    /// کد مختص به مرحومین چند همسر (در صورت وجود).
    /// </summary>
    public int? UniqueId { get; set; }

    /// <summary>
    /// توضیحات مربوط به پرونده.
    /// </summary>
    public string CaseDescription { get; set; }

    /// <summary>امتیاز هدفمندی</summary>
    public float TotalScore { get; set; }
}
