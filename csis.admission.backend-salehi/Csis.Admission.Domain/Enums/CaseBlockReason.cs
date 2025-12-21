namespace Csis.Admission.Domain.Enums;

/// <summary>
/// دلیل انسداد پرونده
/// </summary>
public enum CaseBlockReason : short
{
    /// <summary>انصراف</summary>
    Cancellation = 13,

    /// <summary>
    /// اخراج
    /// </summary>
    Expulsion = 14,

    /// <summary>
    /// مرحوم تکفل ندارد
    /// </summary>
    DeceasedWithoutDependent = 15,

    /// <summary>
    /// خروج از ايران
    /// </summary>
    LeftIran = 16,

    /// <summary>
    /// گزارشهاي رسيده
    /// </summary>
    ReceivedReports = 17,

    /// <summary>
    /// فارغ التحصيلي
    /// </summary>
    Graduation = 18,

    /// <summary>
    /// پرونده راکد
    /// </summary>
    InactiveFile = 19,

    /// <summary>
    /// کد تکراري
    /// </summary>
    DuplicateCode = 20,

    /// <summary>
    /// شاغل مرحوم
    /// </summary>
    DeceasedEmployee = 21,

    /// <summary>
    /// وضعيت پرونده تحصيلي غيرفعال
    /// </summary>
    InactiveEducationalStatus = 22,

    /// <summary>
    /// مصوبه کميسيون پذيرش
    /// </summary>
    AcceptanceCommissionResolution = 23,

    /// <summary>
    /// دادگاه ویژه
    /// </summary>
    SpecialCourt = 24,

    /// <summary>
    /// نظارت و بازرسی
    /// </summary>
    SupervisionAndInspection = 25,

    /// <summary>
    /// تشخیص مدیر شعبه
    /// </summary>
    BranchManagerDecision = 26,

    /// <summary>
    /// عدم مراجعه
    /// </summary>
    NoVisit = 27,

    /// <summary>
    /// امور صیانتی
    /// </summary>
    ProtectiveMatters = 28
}
