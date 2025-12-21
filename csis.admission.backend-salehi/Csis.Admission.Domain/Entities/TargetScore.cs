using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// امتیاز هدف
/// </summary>
public class TargetScore : BaseEntity
{
    /// <summary> کد مرکز </summary>
    public int Codm { get; set; }
    /// <summary>آیا معلم است؟</summary>
    public bool IsTeacher { get; set; }
    /// <summary>آیا مبلّغ است؟</summary>
    public bool IsPreacher { get; set; }
    /// <summary>آیا پژوهشگر است؟</summary>
    public bool IsResearcher { get; set; }
    /// <summary>امتیاز هدفمندی</summary>
    public float TotalScore { get; set; }
}
