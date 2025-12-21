using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>شغل و درآمد تکفل</summary>
public class DependentEmployment : SoftDeletedBaseEntity, IAuditable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long DependentId { get; set; }

    /// <summary>تکفل</summary>
    public DependentSummary Dependent { get; set; }

    /// <summary>وضعیت اشتغال</summary>
    public bool? IsEmployee { get; set; }

    /// <summary>نام محل کار</summary>
    public string EmployeeName { get; set; }

    /// <summary>آدرس محل کار</summary>
    public string EmployeeAddress { get; set; }

    /// <summary>دارای بیمه پایه غیر از مرکز</summary>
    public bool? HasAnotherBaseInsurance { get; set; }

    /// <summary>نوع بیمه پایه</summary>
    public EmploymentInsuranceType? InsuranceType { get; set; } //(base.EmploymentInsuranceType:90)

    /// <summary>نام بیمه پایه</summary>
    public string InsurancePlaceName { get; set; }

    /// <summary>آدرس بیمه پایه</summary>
    public string InsurancePlaceAddress { get; set; }

    /// <summary>دارای بیمه تکمیلی غیر از مرکز</summary>
    public bool? HasAnotherSupInsurance { get; set; }

    /// <summary>روش شناسایی اشتغال</summary>
    public EmploymentReference? Reference { get; set; } //(base.EmploymentReference:719)

    /// <summary> دهک </summary>
    public short? Decile { get; set; }

    /// <inheritdoc/>
    public long? RequestId { get; set; }

    /// <summary>
    /// درخواست
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
