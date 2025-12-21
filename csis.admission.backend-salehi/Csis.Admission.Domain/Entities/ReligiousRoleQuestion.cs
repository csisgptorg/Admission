using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// پرسشنامه نقش آفرینی
/// </summary>
public sealed class ReligiousRoleQuestion : SoftDeletedBaseEntity
{
    /// <summary> </summary>
    public int Codm { get; set; }

    /// <summary>
    /// آیا ملبس است ؟
    /// </summary>
    public bool IsReligiouslyDressed { get; set; }

    /// <summary>
    /// توضیحات ملبس - اجباری
    /// </summary>
    public string ReligiouslyDressedDescription { get; set; }

    /// <summary>
    /// فعالیت در زمینه های حوزوی
    /// </summary>
    public bool HasRole { get; set; }

    /// <summary>
    /// توضیحات فعالیت در زمینه های حوزوی
    /// </summary>
    public string HasRoleDescription { get; set; }

    /// <summary>
    /// نوع نقش آفرینی
    /// </summary>
    public ReligiousRoleType? ReligiousRoleType { get; set; }

    /// <summary>
    /// دلیل عدم فعالیت
    /// </summary>
    public NotHavingRoleCause? NotHavingRoleCause { get; set; }
}
