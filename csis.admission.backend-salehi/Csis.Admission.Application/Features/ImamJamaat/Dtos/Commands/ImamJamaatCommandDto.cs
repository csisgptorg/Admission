using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;

/// <summary>  
/// اطلاعات امام جماعت  
/// </summary>  
public sealed record ImamJamaatCommandDto : BaseCommandDto<ImamJamaatCommandDto, Domain.Entities.ImamJamaat>
{
    /// <summary>  
    /// کد ملی امام جماعت  
    /// </summary>  
    public string NationalCode { get; init; }

    /// <summary>  
    /// کد مرکز مرتبط با امام جماعت  
    /// </summary>  
    public int CodM { get; init; }

    /// <summary>  
    /// نوع حضور امام جماعت در مسجد  
    /// </summary>  
    public PresenceType DailyPresenceType { get; init; }

    /// <summary>
    /// وضعیت فعالیت سالانه امام جماعت
    /// </summary>
    public AnnualActivityType ImamAnnualActivityStatus { get; init; }

    /// <summary>
    /// میانگین ساعت حضور روزانه امام جماعت در مسجد
    /// </summary>
    public float? AverageDailyPresenceHours { get; init; }

    /// <summary>  
    /// سال شروع به امامت در مسجد  
    /// </summary>  
    public DateTime? StartYear { get; init; }

    /// <summary>
    /// سال پایان امامت در مسجد
    /// </summary>
    public DateTime? EndYear { get; init; }

    /// <summary>
    /// در حال امامت است؟
    /// </summary>
    public bool? IsCurrentlyImam { get; init; }

    /// <summary>
    /// آیا امام جماعت از مسجد کمک مالی دریافت می‌کند؟
    /// </summary>
    public bool IsReceivingMonthlyPaymentFromMosque { get; init; }

    /// <summary>  
    /// مبلغ دریافتی ماهانه از مسجد  
    /// </summary>  
    public decimal? MonthlyPaymentFromMosque { get; init; }

    /// <summary>
    /// آیا امام جماعت از سازمان‌ها کمک مالی دریافت می‌کند؟
    /// </summary>
    public bool? IsReceivingMonthlyPaymentFromOrganizations { get; init; }

    /// <summary>
    /// مبلغ دریافتی ماهانه از سازمان‌ها
    /// </summary>
    public decimal? MonthlyPaymentFromOrganizations { get; init; }

    /// <summary>
    /// آیا همسر امام جماعت در همان مسجد فعال است؟
    /// </summary>
    public bool? IsSpouseActiveInSameMosque { get; init; }

    /// <summary>
    /// آیا امام جماعت از سازمان‌ها کمک مالی دریافت می‌کند؟
    /// </summary>
    public bool IsReceivingMonthlyNonCashAssistance { get; init; }

    /// <summary>  
    /// مبلغ دریافتی ماهانه از سازمان‌ها  
    /// </summary>  
    public List<short> MonthlyNonCashAssistance { get; init; }

    /// <summary>  
    /// نهاد صادرکننده حکم امام جماعت  
    /// </summary>  
    public AppointedByType AppointedBy { get; init; }

    /// <summary>
    /// (نام نهاد صادرکننده حکم (سایر نهاد ها
    /// </summary>
    public string? AppointedByOtherOrganization { get; init; }

    /// <summary>
    /// آیا منتسب هیئت امناء است؟
    /// </summary>
    public bool? IsTrusteesBoardMember { get; init; }

    /// <summary>  
    /// آیا گزارش فعالیت به نهادی ارائه شده است؟  
    /// </summary>  
    public bool? ReportsSubmitted { get; init; }

    /// <summary>
    /// نهاد دریافت‌کننده گزارش
    /// </summary>
    public AppointedByType? ReportingOrganizationType { get; init; }

    /// <summary>  
    /// نام نهاد دریافت‌کننده گزارش (در صورت وجود)  
    /// </summary>  
    public string? ReportingOrganization { get; init; }

    /// <summary>
    /// لیست همسران فعال امام جماعت در مسجد
    /// </summary>
    public List<ImamJamaatDependentCommandDto>? ActiveSpousesInMosque { get; init; }

    public override void ReverseCustomMappings(IMappingExpression<ImamJamaatCommandDto, Domain.Entities.ImamJamaat> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(x => x.MonthlyNonCashAssistance,
            opt => opt.MapFrom(src => src.MonthlyNonCashAssistance != null ? src.MonthlyNonCashAssistance.Select(id => id).ToList() : new List<short>()));
    }
}
