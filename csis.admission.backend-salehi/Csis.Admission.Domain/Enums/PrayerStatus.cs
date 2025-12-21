namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت امام جماعت در وعده‌ خاص
/// </summary>
public enum PrayerStatus : short
{
    /// <summary>
    /// امام جماعت روحانی
    /// </summary>
    Clergy = 1,

    /// <summary>
    /// امام جماعت غیرروحانی
    /// </summary>
    NonClergy = 2,

    /// <summary>
    /// فاقد امام جماعت
    /// </summary>
    None = 3
}
