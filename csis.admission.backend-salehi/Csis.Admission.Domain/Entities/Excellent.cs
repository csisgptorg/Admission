using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// ممتازین
/// </summary>
public class Excellent : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }
    
    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public short? EducationYearId { get; set; }

    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public ExcellentEducationYear EducationYear { get; set; }

    /// <summary>
    /// آیدی مقطع تحصیلی
    /// </summary>
    public short? EducationLevelId { get; set; }

    /// <summary>
    /// آیدی مقطع تحصیلی
    /// </summary>
    public ExcellentEducationLevel EducationLevel { get; set; }

    /// <summary>
    /// معدل
    /// </summary>
    public double? Average { get; set; }

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
