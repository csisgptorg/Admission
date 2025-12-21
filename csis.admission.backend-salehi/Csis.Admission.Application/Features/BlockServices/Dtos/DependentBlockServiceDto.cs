using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Dtos;

/// <summary>دریافت</summary>
public sealed record DependentBlockServiceDto : BaseDto<DependentBlockServiceDto, DependentBlockService>
{
    /// <summary>تکفل</summary>
    public string Dependent { get; init; }

    /// <summary>نسبت</summary>
    public DependentRelation? Relation { get; set; }

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
    public override void CustomMappings(IMappingExpression<DependentBlockService, DependentBlockServiceDto> mapping) {
        mapping.ForMember(dto => dto.Relation, config => config.MapFrom(model => model.Dependent.Relation));
        mapping.ForMember(dto => dto.Dependent, config => config.MapFrom(model => model.Dependent.FullName));
        mapping.ForMember(dto => dto.BlockDate, config => config.MapFrom(model => model.BlockDate.IntDateToString()));
        mapping.ForMember(dto => dto.Service, config => config.MapFrom(model => model.Service.Title));
    }
}
