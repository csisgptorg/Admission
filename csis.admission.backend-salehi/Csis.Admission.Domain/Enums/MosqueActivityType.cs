namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت فعالیت کلی مسجد
/// </summary>
public enum MosqueActivityType : short
{
    /// <summary>
    /// نماز جماعت همراه با فعالیت فرهنگی، اجتماعی و دینی
    /// </summary>
    PrayerWithSocialCultural = 1,

    /// <summary>
    /// فقط نماز جماعت
    /// </summary>
    OnlyPrayer = 2,
}
