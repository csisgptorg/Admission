using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.BlockServices.Dtos;

/// <summary>دریافت</summary>
public sealed record StudentBlockServiceDto : BaseDto<StudentBlockServiceDto, StudentBlockService>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>تاریخ انسداد</summary>
    public string BlockDate { get; init; }

    /// <summary>علت</summary>
    public string Reason { get; init; }

    /// <summary>شناسه خدمت</summary>
    public int ServiceId { get; init; }

    /// <summary>خدمت</summary>
    public string Service { get; init; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<StudentBlockService, StudentBlockServiceDto> mapping) {
        mapping.ForMember(dto => dto.BlockDate, config => config.MapFrom(model => model.BlockDate.IntDateToString()));
        mapping.ForMember(dto => dto.Service, config => config.MapFrom(model => model.Service.Title));
    }
}
