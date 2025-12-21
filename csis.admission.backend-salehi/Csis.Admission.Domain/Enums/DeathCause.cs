namespace Csis.Admission.Domain.Enums;

/// <summary>
/// علت فوت
/// </summary>
public enum DeathCause : byte
{
    /// <summary>
    /// مرگ طبيعي
    /// </summary>
    NaturalDeath = 1,

    /// <summary>
    /// سوانح
    /// </summary>
    Accidents = 2,

    /// <summary>
    /// سرطان
    /// </summary>
    Cancer = 3,

    /// <summary>
    /// قلبي - عروقي
    /// </summary>
    Cardiovascular = 4,

    /// <summary>
    /// شهيد
    /// </summary>
    Martyr = 5,

    /// <summary>
    /// کرونا
    /// </summary>
    Coronavirus = 6,

    /// <summary>
    /// مشکوک به کرونا
    /// </summary>
    SuspectedOfCoronavirus = 7
}
