using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// فعالیت‌های مسجد
/// </summary>
public class MosqueActivity : SoftDeletedBaseEntity
{
    /// <summary>
    /// شناسه مسجد مرتبط با فعالیت
    /// </summary>
    public int? MosqueId { get; set; }

    /// <summary>
    /// مسجد مرتبط با فعالیت
    /// </summary>
    public Mosque Mosque { get; set; }

    /// <summary>
    /// لیست وعده‌های نماز برگزار شد
    /// </summary>
    public List<PrayerTimes> PrayerTimes { get; set; }

    /// <summary>
    /// سخنرانی‌های منظم
    /// </summary>
    public bool? RegularLectures { get; set; }

    /// <summary>
    /// لیست برنامه‌های قرآنی انجام شد
    /// </summary>
    public List<QuranProgram> QuranPrograms { get; set; }

    /// <summary>
    /// لیست کلاس‌های آموزشی فرهنگی، ورزشی، علمی و مهارت
    /// </summary>
    public List<EducationalClass> EducationalClasses { get; set; }

    /// <summary>
    /// وضعیت امام جماعت در وعده‌ صبح
    /// </summary>
    public PrayerStatus? MorningPrayerStatus { get; set; }

    /// <summary>
    /// وضعیت امام جماعت در وعده‌ ظهر
    /// </summary>
    public PrayerStatus? NoonPrayerStatus { get; set; }

    /// <summary>
    /// وضعیت امام جماعت در وعده‌ مغرب
    /// </summary>
    public PrayerStatus? EveningPrayerStatus { get; set; }

    /// <summary>
    /// وضعیت کلی فعالیت مسجد
    /// </summary>
    public MosqueActivityType ActivityStatus { get; set; }

    /// <summary>
    /// لیست مسئولین برگزاری کلاس‌ها و برنامه‌های مسج
    /// </summary>
    public List<OrganizerOfClassesAndProgramsInMosque> OrganizerOfClassesAndProgramsInMosque { get; set; }

    /// <summary>
    /// (مسئول برگزاری کلاس‌ها و برنامه‌های مسجد (سایر موارد
    /// </summary>
    public string? OrganizerOfClassesAndProgramsInMosqueOther { get; set; }
}
