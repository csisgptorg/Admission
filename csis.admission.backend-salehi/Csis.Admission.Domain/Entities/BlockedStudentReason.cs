using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت دلیل مسدودی
/// </summary>
public sealed class BlockedStudentReason : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// شناسه طلبه
    /// </summary>
    public int StudentCodm { get; set; }

    /// <summary>
    /// دلیل
    /// </summary>
    public short Reason { get; set; }

    /// <inheritdoc/>>
    public string[] GetFilterableFields() {
        return [];
    }
}
