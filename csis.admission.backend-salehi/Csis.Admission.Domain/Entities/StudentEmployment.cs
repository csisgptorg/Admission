using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>شغل و درآمد</summary>
public class StudentEmployment : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>آیا فرد دارای درآمد است؟</summary>
    public bool? HasIncome { get; set; }

    /// <summary>آیا فرد کارمند است؟</summary>
    public bool? IsEmployee { get; set; }

    /// <summary>نام محل کار فرد</summary>
    public string EmployeeName { get; set; }

    /// <summary>آدرس محل کار فرد</summary>
    public string EmployeeAddress { get; set; }

    /// <summary>آیا فرد دارای درآمد کافی می‌باشد؟</summary>
    public bool? HasSufficientIncome { get; set; }

    /// <summary>آیا فرد دارای بیمه پایه دیگری است؟</summary>
    public bool? HasAnotherBaseInsurance { get; set; }

    /// <summary>نام محل بیمه پایه دیگر</summary>
    public string InsurancePlaceName { get; set; }

    /// <summary>آدرس محل بیمه پایه دیگر</summary>
    public string InsurancePlaceAddress { get; set; }

    /// <summary>آیا فرد دارای بیمه تکمیلی دیگری است؟</summary>
    public bool? HasAnotherSupInsurance { get; set; }

    /// <summary>آیا فرد در حوزه مشغول به کار است؟</summary>
    public bool? IsEmployeeInHowze { get; set; }

    /// <summary>نوع اشتغال در حوزه (شناسه نوع اشتغال)</summary>
    public EmploymentHowzeType? HowzeTypeId { get; set; } // base.EmploymentHowzeType : 683

    /// <summary>آیا فرد بازنشسته است؟</summary>
    public bool? IsRetried { get; set; }

    /// <summary>نوع بیمه اشتغال (شناسه نوع بیمه)</summary>
    public EmploymentInsuranceType? InsuranceTypeId { get; set; } // base.EmploymentInsuranceType : 90

    /// <summary>مرجع یا منبع اشتغال</summary>
    public EmploymentReference? Reference { get; set; }

    /// <summary>دهک درآمدی فرد</summary>
    public short? Decile { get; set; }

    /// <inheritdoc/>
    public long? RequestId { get; set; }

    /// <summary>
    /// درخواست مرتبط
    /// </summary>
    public Request Request { get; set; }

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
