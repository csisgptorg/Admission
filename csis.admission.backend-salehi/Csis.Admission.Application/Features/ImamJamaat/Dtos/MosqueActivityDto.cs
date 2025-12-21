using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos;

/// <summary>
/// اطلاعات فعالیت‌های عبادی و فرهنگی مسجد
/// </summary>
public sealed record MosqueActivityDto : BaseDto<MosqueActivityDto, MosqueActivity>
{
    /// <summary>
    /// وعده‌های نماز برگزار شده (ارسال به صورت لیست اعداد: [1,2])
    /// </summary>
    public List<short> PrayerTimesFlags { get; init; }

    /// <summary>
    /// آیا سخنرانی منظم برگزار می‌شود؟
    /// </summary>
    public bool? RegularLectures { get; init; }

    /// <summary>
    /// برنامه‌های قرآنی (مثلاً [1,4])
    /// </summary>
    public List<short> QuranProgramFlags { get; init; }

    /// <summary>
    /// کلاس‌های فرهنگی، علمی، ورزشی، مهارتی (مثلاً [1,8])
    /// </summary>
    public List<short> EducationalClassFlags { get; init; }

    /// <summary>
    /// وضعیت امام جماعت در وعده‌ صبح
    /// </summary>
    public PrayerStatus MorningPrayerStatus { get; init; }

    /// <summary>
    /// وضعیت امام جماعت در وعده‌ ظهر
    /// </summary>
    public PrayerStatus NoonPrayerStatus { get; init; }

    /// <summary>
    /// وضعیت امام جماعت در وعده‌ مغرب
    /// </summary>
    public PrayerStatus EveningPrayerStatus { get; init; }

    /// <summary>
    /// وضعیت کلی فعالیت مسجد
    /// </summary>
    public MosqueActivityType ActivityStatus { get; init; }

    /// <summary>
    /// مسئول برگزاری کلاس‌ها و برنامه‌های مسجد
    /// </summary>
    public List<short> OrganizerOfClassesAndProgramsInMosque { get; init; }

    /// <summary>
    /// (مسئول برگزاری کلاس‌ها و برنامه‌های مسجد (سایر موارد
    /// </summary>
    public string? OrganizerOfClassesAndProgramsInMosqueOther { get; init; }

    public override void CustomMappings(IMappingExpression<MosqueActivity, MosqueActivityDto> mapping)
    {
        base.CustomMappings(mapping);
        
        mapping.ForMember(dest => dest.PrayerTimesFlags,
                opt => opt.MapFrom(src => src.PrayerTimes != null ? src.PrayerTimes.Select(e => Convert.ToInt16(e)).ToList() : new List<short>()))
            .ForMember(dest => dest.QuranProgramFlags,
                opt => opt.MapFrom(src => src.QuranPrograms != null ? src.QuranPrograms.Select(e => Convert.ToInt16(e)).ToList() : new List<short>()))
            .ForMember(dest => dest.EducationalClassFlags,
                opt => opt.MapFrom(src => src.EducationalClasses != null ? src.EducationalClasses.Select(e => Convert.ToInt16(e)).ToList() : new List<short>()))
            .ForMember(dest => dest.OrganizerOfClassesAndProgramsInMosque,
                opt => opt.MapFrom(src => src.OrganizerOfClassesAndProgramsInMosque != null ? src.OrganizerOfClassesAndProgramsInMosque.Select(e => Convert.ToInt16(e)).ToList() : new List<short>()));
    }
}
