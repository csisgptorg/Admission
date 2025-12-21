using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// تبليغ
/// </summary>
public class Preach : SoftDeletedBaseEntity, IFilterable, IAuditable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int? Codm { get; set; }

    /// <summary>
    /// کشور
    /// </summary>
    public short? CountryId { get; set; }

    /// <summary>
    /// کشور
    /// </summary>
    public Country Country { get; set; }

    /// <summary>
    /// استان
    /// </summary>
    public short? ProvinceId { get; set; }

    /// <summary>
    /// استان
    /// </summary>
    public Province Province { get; set; }

    ///// <summary>
    ///// شهر
    ///// </summary>
    //public int? CityId { get; set; }

    ///// <summary>
    ///// شهر
    ///// </summary>
    //public City City { get; set; }

    /// <summary>
    /// شهر
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public int? StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public int? EndDate { get; set; }

    /// <summary>
    /// نوع تبلیغ
    /// </summary>
    public PreachKind? Kind { get; set; }

    /// <summary>
    /// محل صدور مدرک
    /// </summary>
    public PreachApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; set; }

    /// <inheritdoc/>
    public Guid? TempId { get; set; }

    /// <inheritdoc/>
    public DataSource? AuditDataSource { get; set; }

    /// <inheritdoc/>
    public int? AuditRequestId { get; set; }

    /// <inheritdoc/>
    public int? AuditPersonId { get; set; }

    /// <summary>مدت زمان تبلیغ به روز</summary>
    public short? DurationInDays { get; set; }

    /// <summary>
    /// Filterable Fields
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string[] GetFilterableFields() {
        return [nameof(Codm)];
    }
}
