using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.CulturalActivityGrades.Dtos;

/// <summary>
/// رتبه فعالیت فرهنگی
/// </summary>
public sealed record CulturalActivityGradeDto : BaseDto<CulturalActivityGradeDto, CulturalActivityGrade>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter ApprovalCenter { get; set; }

    /// <summary>
    /// رتبه
    /// </summary>
    public short Grade { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    public string RegisterDate { get; set; }

    /// <summary>
    /// تاریخ اعتبار
    /// </summary>
    public string ExpirationDate { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<CulturalActivityGrade, CulturalActivityGradeDto> mapping) {
        mapping.ForMember(dto => dto.RegisterDate, config => config.MapFrom(model => model.RegisterDate.IntDateToString()));
        mapping.ForMember(dto => dto.ExpirationDate, config => config.MapFrom(model => model.ExpirationDate.IntDateToString()));
    }
}
