using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.CommissionInfos.Dtos;

/// <summary>کمسیون طلبه</summary>
public record StudentCommissionInfoDto : BaseDto<StudentCommissionInfoDto, CommissionInfo>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>طلبه</summary>
    public string Student { get; init; }

    /// <summary>وضعیت درخواست</summary>
    public string RequestStatus { get; init; }

    /// <summary>نوع</summary>
    public string Type { get; init; }

    /// <summary>تاریخ ساخت</summary>
    public string CreateDate { get; init; }

    /// <summary></summary>
    public string Creator { get; init; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<CommissionInfo, StudentCommissionInfoDto> mapping) {
        mapping.ForMember(dto => dto.CreateDate, config => config.MapFrom(model => model.CreateDate.IntDateToString()));
        mapping.ForMember(dto => dto.Creator, config => config.MapFrom(model => model.CreatorFullName));
    }
}
