using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>تحصیلات دانشگاهی</summary>
public class UniversityEducation : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>کد ملی</summary>
    public int Codm { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <summary>تکفل</summary>
    public DependentSummary Dependent { get; set; }

    /// <summary>در حال تحصیل</summary>
    public bool InStudy { get; set; }

    /// <summary>مدرک تحصیلی</summary>
    public StudyLevel? StudyLevel { get; set; }

    /// <summary>رشته تحصیلی</summary>
    public string CourseStudy { get; set; }

    /// <summary>نوع دانشگاه</summary>
    public UniversityTypeEnum? UniversityType { get; set; }

    /// <summary>دانشگاه</summary>
    public string UniversityName { get; set; }

    /// <summary>استان</summary>
    public string ProvinceTitle { get; set; }

    /// <summary>تاریخ شروع</summary>
    public int? StartDate { get; set; }

    /// <summary>تاریخ پایان</summary>
    public int? EndDate { get; set; }

    /// <summary>معدل</summary>
    public double? Average { get; set; }

    /// <summary>تاریخ اعتبار</summary>
    public int? ValidityDate { get; set; }

    /// <summary>شناسه درخواست</summary>
    public long? RequestId { get; set; }

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
