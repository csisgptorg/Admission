using Csis.Admission.Application.Common.Dtos;
using static Csis.Admission.Domain.Entities.Famous;

namespace Csis.Admission.Application.Features.Famouses.Dtos;

/// <summary>مشهور</summary>
public sealed record StudentFamousDto : BaseDto<StudentFamousDto, Famous>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>نوع</summary>
    public TypeEnum Type { get; set; }
    /// <summary>محدوده</summary>
    public AreaEnum Area { get; set; }
    /// <summary>نقش</summary>
    public RoleEnum? Role { get; set; }
    /// <summary>سمت</summary>
    public string Position { get; set; }
    /// <summary></summary>
    public string ActionPlace { get; set; }
}
