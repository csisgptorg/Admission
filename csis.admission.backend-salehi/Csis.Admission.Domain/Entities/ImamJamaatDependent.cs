using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// امام جماعت وابسته
/// </summary>
public class ImamJamaatDependent : BaseEntity<long>
{
    /// <summary>
    /// شناسه همسر امام جماعت
    /// </summary>
    public long DependentId { get; set; }

    /// <summary>
    /// شناسه امام جماعت
    /// </summary>
    public int ImamJamaatId { get; set; }

    /// <summary>
    /// امام جماعت مرتبط
    /// </summary>
    public ImamJamaat ImamJamaat { get; set; }
}
