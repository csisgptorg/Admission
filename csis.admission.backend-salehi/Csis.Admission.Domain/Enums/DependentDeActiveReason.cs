namespace Csis.Admission.Domain.Enums;

/// <summary>
/// دلیل انسداد پرونده
/// </summary>
public enum DependentDeActiveReasonEnum : short
{
    /// <summary>
    /// اتمام سن
    /// </summary>
    AgeExpiration = 1,

    /// <summary>
    /// ازدواج
    /// </summary>
    Marriage = 2,

    /// <summary>
    /// اشتغال
    /// </summary>
    Employment = 3,

    /// <summary>
    /// مرحوم
    /// </summary>
    Deceased = 4,

    /// <summary>
    /// خروج از ايران
    /// </summary>
    ExitFromIran = 5,

    /// <summary>
    /// کد مستقل
    /// </summary>
    IndependentCode = 6,

    /// <summary>
    /// طلاق
    /// </summary>
    Divorce = 7,

    /// <summary>
    /// فاقد مدارک
    /// </summary>
    LackOfDocuments = 8,

    /// <summary>
    /// ازدواج و انتقال
    /// </summary>
    MarriageAndTransfer = 22,

    /// <summary>
    /// طلاق همسر و انتقال
    /// </summary>
    SpouseDivorceAndTransfer = 23,

    /// <summary>
    /// فرزند طلاق
    /// </summary>
    DivorceChild = 26,

    /// <summary>
    /// انتقال به کد ديگر
    /// </summary>
    TransferToAnotherCode = 27,

    /// <summary>
    /// بدسرپرست
    /// </summary>
    Orphan = 28,

    /// <summary>
    /// عدم دسترسي
    /// </summary>
    NoAccess = 30,

    /// <summary>
    /// انسداد سرپرست
    /// </summary>
    GuardianBlock = 31,

    /// <summary>
    /// صرفا عضو خانواده
    /// </summary>
    OnlyFamilyMember = 34,

    /// <summary>
    /// فاقد شرايط
    /// </summary>
    NotQualified = 35,

    /// <summary>
    /// اتمام اقامت
    /// </summary>
    ResidenceExpiration = 37,

    /// <summary>
    /// طلاق بعد از فوت پدر
    /// </summary>
    DivorceAfterFatherDeath = 39,

    /// <summary>
    /// ازدواج بعد از فوت همسر
    /// </summary>
    MarriageAfterSpouseDeath = 40,

    /// <summary>
    /// ازدواج بعد از فوت همسر و انتقال
    /// </summary>
    MarriageAfterSpouseDeathAndTransfer = 42,

    /// <summary>
    /// درخواست طلبه
    /// </summary>
    SeminarianRequest = 43,

    /// <summary>
    /// ازدواج و کدمستقل
    /// </summary>
    MarriageAndIndependentCode = 45,

    /// <summary>
    /// طلاق با تمکن مالي
    /// </summary>
    DivorceWithFinancialCapability = 46,

    /// <summary>
    /// فاقد شرايط به تشخيص کاربر ارشد
    /// </summary>
    ConditionsNotMetBySeniorUser = 48,

    /// <summary>
    /// فاقد کد يکتا
    /// </summary>
    LackOfUniqueCode = 50

}
