using Csis.Admission.Domain.Common;
using Csis.Utilities.Annotations;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت تحصیلات حوزوی
/// </summary>
public sealed class StudentEducation : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// شناسه طلبه
    /// </summary>
    public int StudentCodm { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public byte? ApprovalCenter { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public long? NumberInApprovalCenter { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string StatusInCenter { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public byte? StudyStatus { get; set; }

    /// <summary>
    /// سال ورود به حوزه
    /// </summary>
    public int? EnteringYear { get; set; }

    /// <summary>
    /// پایه
    /// </summary>
    public short? Grade { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public float? ProgressRate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? HasReligiousDressDegree { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? ReligiousDressDegreeDate { get; set; }

    /// <summary>
    /// طلبه
    /// </summary>
    [ForeignKey(nameof(StudentCodm))]
    public Student StudentCodmNavigation { get; private set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [];
    }
}
