using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Veterans.Dtos;

/// <summary>
/// ایثارگری
/// </summary>
public sealed record VeteranDto : BaseDto<VeteranDto, Veteran>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تعداد روز دفاع از حرم
    /// </summary>
    public int? HaramDefenceDays { get; set; }

    /// <summary>مدت دفاع از حرم</summary>
    public string? HaramDefenceDuration=> GetDuration(HaramDefenceDays);

    /// <summary>
    /// تعداد روز دفاع مقدس
    /// </summary>
    public int? HolyDefenseDays { get; set; }

    /// <summary>مدت دفاع مقدس</summary>
    public string? HolyDefenseDuration => GetDuration(HolyDefenseDays);

    /// <summary>
    /// تعداد روز آزادگی
    /// </summary>
    public int? CaptivityDays { get; set; }

    /// <summary>مدت آزادگی</summary>
    public string? CaptivityDuration => GetDuration(CaptivityDays);

    /// <summary>
    /// تعداد روز زندان قبل از انقلاب
    /// </summary>
    public int? JailDays { get; set; }

    /// <summary>مدت زندان قبل از انقلاب</summary>
    public string? JailDuration => GetDuration(JailDays);

    /// <summary>
    /// تعداد روز تبعید قبل از انقلاب
    /// </summary>
    public int? ExileDays { get; set; }

    /// <summary>مدت تبعید قبل از انقلاب</summary>
    public string? ExileDuration => GetDuration(ExileDays);
    
    /// <summary>
    /// در صد جانبازی
    /// </summary>
    public short? VeteranPercent { get; set; }

    /// <summary>
    ///نسبت با شهید
    /// </summary>
    public DependentRelation? RelationWithMartyr { get; set; }

    /// <summary>
    ///نوع شهادت
    /// </summary>
    public MartyrType? MartyrType { get; set; }

    private static string? GetDuration(int? days) {
        return days > 0
            ? $"{days} روز ({Common.Utilities.ConvertDaysToDurationString(days)})"
            : "";
    }
}
