using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Houses.Dtos;

/// <summary>
/// مسکن اجاره ای
/// </summary>
public sealed record TenantDto : BaseCommandDto<TenantDto, Tenant>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// نام صاحبخانه
    /// </summary>
    public string? HostName { get; init; }

    /// <summary>
    /// موبایل صاحب خانه
    /// </summary>
    public string? HostMobile { get; init; }

    /// <summary>
    /// مبلغ رهن ریال
    /// </summary>
    public long? MortgageAmount { get; init; }

    /// <summary>
    /// مبلغ اجاره ریال
    /// </summary>
    public long? RentAmount { get; init; }

    /// <summary>
    /// تاریخ شروع قرارداد
    /// </summary>
    public string? StartDate { get; init; }

    /// <summary>
    /// تاریخ پایان قرارداد
    /// </summary>
    public string? EndDate { get; init; }

    /// <summary>
    /// کد رهگیری
    /// </summary>
    public string? TrackingCode { get; init; }

    /// <inheritdoc />
    public override void ReverseCustomMappings(IMappingExpression<TenantDto, Tenant> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(x=>x.StartDate, opt => opt.MapFrom(x=>x.StartDate.StringDateToInt()));
        mapping.ForMember(x=>x.EndDate, opt => opt.MapFrom(x=>x.EndDate.StringDateToInt()));
    }
    /// <inheritdoc />
    public override void CustomMappings(IMappingExpression<Tenant, TenantDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.IntDateToString()));
        mapping.ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.IntDateToString()));

    }
}
