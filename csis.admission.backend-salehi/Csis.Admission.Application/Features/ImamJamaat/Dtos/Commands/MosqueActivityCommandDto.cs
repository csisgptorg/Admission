using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;

/// <summary>
/// اطلاعات فعالیت‌های عبادی و فرهنگی مسجد
/// </summary>
public sealed record MosqueActivityCommandDto : BaseCommandDto<MosqueActivityCommandDto, MosqueActivity>
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
    /// نماز صبح
    /// </summary>
    public PrayerStatus? MorningPrayerStatus { get; init; }

    /// <summary>
    /// نماز ظهر
    /// </summary>
    public PrayerStatus? NoonPrayerStatus { get; init; }

    /// <summary>
    /// نماز مغرب
    /// </summary>
    public PrayerStatus? EveningPrayerStatus { get; init; }


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

    public override void ReverseCustomMappings(IMappingExpression<MosqueActivityCommandDto, MosqueActivity> mapping) {
        base.ReverseCustomMappings(mapping);
        
        mapping.ForMember(dest => dest.PrayerTimes,
            opt => opt.MapFrom(src => src.PrayerTimesFlags));
        mapping.ForMember(dest => dest.QuranPrograms,
            opt => opt.MapFrom(src => src.QuranProgramFlags));
        mapping.ForMember(dest => dest.EducationalClasses,
            opt => opt.MapFrom(src => src.EducationalClassFlags));
        mapping.ForMember(dest => dest.OrganizerOfClassesAndProgramsInMosque,
            opt => opt.MapFrom(src => src.OrganizerOfClassesAndProgramsInMosque));
    }
}
