using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Preaches.Dtos;

/// <summary>
/// مدل نمایشی موجودیت ازدواج
/// </summary>
public sealed record PreachDto : BaseDto<PreachDto, Preach>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// کشور
    /// </summary>
    public short? CountryId { get; set; }

    /// <summary>
    /// کشور
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// استان
    /// </summary>
    public short? ProvinceId { get; set; }

    /// <summary>
    /// استان
    /// </summary>
    public string Province { get; set; }

    /// <summary>
    /// شهر
    /// </summary>
    public short? CityId { get; set; }

    /// <summary>
    /// شهر
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public string EndDate { get; set; }

    /// <summary>
    /// نوع تبلیغ
    /// </summary>
    public PreachKind? Kind { get; set; }

    /// <summary>
    /// محل صدور مدرک
    /// </summary>
    public PreachApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; set; }

    /// <summary>مدت زمان تبلیغ به روز</summary>
    public short? DurationInDays { get; set; }

    /// <summary>توضیحات</summary>
    public string Description { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<Preach, PreachDto> mapping) {
        mapping.ForMember(dto => dto.Country, config => config.MapFrom(model => model.Country.Title));
        mapping.ForMember(dto => dto.Province, config => config.MapFrom(model => model.Province.Title));
        mapping.ForMember(dto => dto.StartDate, config => config.MapFrom(model => model.StartDate.IntDateToString()));
        mapping.ForMember(dto => dto.EndDate, config => config.MapFrom(model => model.EndDate.IntDateToString()));
    }
}
