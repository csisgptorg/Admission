namespace Csis.Admission.Domain.Enums;

/// <summary>
/// دلیل اعتبار پرونده
/// </summary>
public enum CaseValidityReason : short
{
    /// <summary>
    /// اشتغال به تحصيل
    /// </summary>
    Studying = 1,
    /// <summary>
    /// تدريس
    /// </summary>
    Teaching = 2,
    /// <summary>
    /// پژوهش
    /// </summary>
    Research = 3,
    /// <summary>
    /// تبليغ
    /// </summary>
    Propagation = 4,
    /// <summary>
    /// معمر
    /// </summary>
    Elderly = 9,
    /// <summary>
    /// ازکارافتاده
    /// </summary>
    Disabled = 10,
    /// <summary>
    /// فوت
    /// </summary>
    Deceased = 12,
    /// <summary>
    /// تمديد موقت تا تشکيل کميسيون
    /// </summary>
    TemporaryExtensionUntilCommissionFormation = 13,
    /// <summary>
    /// سوابق حوزوي
    /// </summary>
    SeminaryRecords = 15,
    /// <summary>
    /// کميسيون
    /// </summary>
    Commission = 16,
    /// <summary>
    /// مشاهير
    /// </summary>
    Celebrities = 18,
    /// <summary>
    /// سوابق تحصيلي
    /// </summary>
    AcademicRecords = 21,
    /// <summary>
    /// شان اجرائي
    /// </summary>
    ExecutiveDignity = 22,
    /// <summary>
    /// ايام بارداري
    /// </summary>
    PregnancyDays = 23,
    /// <summary>
    /// سرباز طلبه
    /// </summary>
    SoldierCleric = 24
}
