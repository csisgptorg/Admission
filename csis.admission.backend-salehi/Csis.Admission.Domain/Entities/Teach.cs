using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// تدريس
/// </summary>
public class Teach : SoftDeletedBaseEntity, IFilterable, IAuditable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public short? ProvinceId { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public Province Province { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public short? CityId { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public City City { get; set; }

    /// <summary>
    /// EducationYear
    /// </summary>
    public short? EducationYearId { get; set; }

    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public EducationYear EducationYear { get; set; }

    /// <summary>
    /// نیم سال تحصیلی
    /// </summary>
    public EducationSemester? EducationSemester { get; set; }

    /// <summary>
    /// مقطع تحصیلی که در آن تدریس میشود
    /// </summary>
    public TeachEducationLevel? EducationLevel { get; set; }

    /// <summary>
    /// Lesson
    /// </summary>
    public string Lesson { get; set; }

    /// <summary>
    /// SchoolId
    /// </summary>
    public short? SchoolId { get; set; }

    /// <summary>
    /// School
    /// </summary>
    public School School { get; set; }

    /// <summary>
    /// WeekSession
    /// </summary>
    public short? WeekSession { get; set; }

    /// <summary>
    /// شناسه درخواست کمیسیون
    /// </summary>
    public int? CommissionRequestId { get; set; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; set; }  

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; set; }

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

    /// <inheritdoc/>>
    /// <exception cref="NotImplementedException"></exception>
    public string[] GetFilterableFields() {
        return [nameof(Codm)];
    }
}
