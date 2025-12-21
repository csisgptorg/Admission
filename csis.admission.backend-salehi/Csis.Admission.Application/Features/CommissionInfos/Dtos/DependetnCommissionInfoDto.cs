using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.CommissionsInfos.Dtos;

/// <summary>کمسیون تکفل</summary>
public record DependentCommissionInfoDto : BaseDto<DependentCommissionInfoDto, CommissionInfo>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>طلبه</summary>
    public string Student { get; init; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>تکفل</summary>
    public string Dependent { get; init; }

    /// <summary>نسبت</summary>
    public DependentRelation Relation { get; init; }

    /// <summary>وضعیت درخواست</summary>
    public string RequestStatusTitle { get; init; }
    /// <summary>وضعیت درخواست</summary>
    public short RequestStatus { get; init; }
    /// <summary>تاریخ اعتبار کمسیون</summary>
    public int? CommissionValidityDate { get; init; }

    /// <summary>نوع</summary>
    public string Type { get; init; }

    /// <summary>تاریخ ساخت</summary>
    public string CreateDate { get; init; }

    /// <summary></summary>
    public string Creator { get; init; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<CommissionInfo, DependentCommissionInfoDto> mapping) {
        mapping.ForMember(dto => dto.CreateDate, config => config.MapFrom(model => model.CreateDate.IntDateToString()));
        mapping.ForMember(dto => dto.Creator, config => config.MapFrom(model => model.CreatorFullName));
    }
}
