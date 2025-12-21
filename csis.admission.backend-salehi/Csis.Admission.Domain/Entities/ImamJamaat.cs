using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;
/// <summary>
/// امام جماعت
/// </summary>
public class ImamJamaat : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>  
    /// کد ملی امام جماعت  
    /// </summary>  
    public string NationalCode { get; set; }
    /// <summary>
    /// نام و نام خانوادگی امام جماعت
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// کد مرکز
    /// </summary>
    public int CodM { get; set; }

    /// <summary>
    /// نوع حضور امام جماعت در مسجد
    /// </summary>
    public PresenceType DailyPresenceType { get; set; }

    /// <summary>
    /// میانگین ساعت حضور روزانه امام جماعت در مسجد
    /// </summary>
    public float? AverageDailyPresenceHours { get; set; }

    /// <summary>
    /// وضعیت فعالیت سالانه امام جماعت
    /// </summary>
    public AnnualActivityType ImamAnnualActivityStatus { get; set; }

    /// <summary>
    /// سال شروع به امامت در مسجد
    /// </summary>
    public DateTime StartYear { get; set; }

    /// <summary>
    /// سال پایان امامت در مسجد
    /// </summary>
    public DateTime? EndYear { get; set; }

    /// <summary>
    /// در حال امامت است؟
    /// </summary>
    public bool? IsCurrentlyImam { get; set; }

    /// <summary>
    /// آیا امام جماعت از مردم کمک مالی دریافت می‌کند؟
    /// </summary>
    public bool IsReceivingMonthlyPaymentFromMosque { get; set; }

    /// <summary>
    /// مبلغ دریافتی ماهانه از مردم
    /// </summary>
    public decimal? MonthlyPaymentFromMosque { get; set; }

    /// <summary>
    /// آیا امام جماعت از سازمان‌ها کمک مالی دریافت می‌کند؟
    /// </summary>
    public bool? IsReceivingMonthlyPaymentFromOrganizations { get; set; }

    /// <summary>
    /// مبلغ دریافتی ماهانه از سازمان‌ها
    /// </summary>
    public decimal? MonthlyPaymentFromOrganizations { get; set; }

    /// <summary>
    /// آیا همسر امام جماعت در همان مسجد فعال است؟
    /// </summary>
    public bool? IsSpouseActiveInSameMosque { get; set; }

    /// <summary>
    /// آیا امام جماعت کمک غیرنقدی دریافت می‌کند؟
    /// </summary>
    public bool IsReceivingMonthlyNonCashAssistance { get; set; }

    /// <summary>
    /// لیست کمک‌های غیرنقدی دریافتی ماهانه از مسجد
    /// ⚠️ تبدیل خودکار توسط EF Core در Configuration انجام می‌شود
    /// </summary>
    public List<NonCashAssistanceFromMosque> MonthlyNonCashAssistance { get; set; }

    /// <summary>
    /// نهاد صادرکننده حکم امام جماعت
    /// </summary>
    public AppointedByType AppointedBy { get; set; }

    /// <summary>
    /// (نام نهاد صادرکننده حکم (سایر نهاد ها
    /// </summary>
    public string? AppointedByOtherOrganization { get; set; }

    /// <summary>
    /// آیا منتسب هیئت امناء است؟
    /// </summary>
    public bool? IsTrusteesBoardMember { get; set; }

    /// <summary>
    /// آیا گزارش فعالیت به نهادی ارائه می‌شود؟
    /// </summary>
    public bool? ReportsSubmitted { get; set; }

    /// <summary>
    /// نهاد دریافت‌کننده گزارش
    /// </summary>
    public AppointedByType? ReportingOrganizationType { get; set; }

    /// <summary>
    /// (نام نهاد دریافت‌کننده گزارش (در صورت وجود
    /// </summary>
    public string? ReportingOrganization { get; set; }



    /// <summary>
    /// شناسه مسجد مرتبط با امام جماعت
    /// </summary>
    public int MosqueId { get; set; }

    /// <summary>
    /// مسجد مرتبط با امام جماعت
    /// </summary>
    public Mosque Mosque { get; set; }

    /// <summary>
    /// لیست همسران فعال امام جماعت در مسجد
    /// </summary>
    public List<ImamJamaatDependent>? ActiveSpousesInMosque { get; set; }

    /// <summary>
    /// دریافت لیست فیلدهایی که قابلیت فیلترگذاری داینامیک دارند
    /// </summary>
    /// <returns></returns>
    public string[] GetFilterableFields() {
        return [$"Mosque.OfficialName", "Mosque.CreatedOn", nameof(CodM), nameof(FullName), nameof(NationalCode)];
    }

    /// <summary>
    /// تنظیم نام کامل امام جماعت
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    public void SetFullName(string firstName, string lastName) => FullName = $"{firstName} {lastName}";
}
