using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>مشهور</summary>
public class Famous : SoftDeletedBaseEntity
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>نوع</summary>
    public TypeEnum Type { get; set; }
    /// <summary>محدوده</summary>
    public AreaEnum Area { get; set; }
    /// <summary>نقش</summary>
    public RoleEnum? Role { get; set; }
}
