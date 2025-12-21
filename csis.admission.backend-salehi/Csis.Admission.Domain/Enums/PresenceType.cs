namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع حضور امام جماعت در مسجد
/// </summary>
public enum PresenceType : short
{
    /// فقط اقامه نماز
    PrayerOnly = 1,
    /// نماز و فعالیت تبلیغی
    PrayerAndPromotional = 2
}
