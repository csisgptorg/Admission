namespace Csis.Admission.Domain.Enums;

/// <summary>
/// دلیل غیر فعال بودن پرونده
/// </summary>
public enum DependentActiveReasonEnum : short
{
    /// <summary>
    /// محصل
    /// </summary>
    Student = 12,
    /// <summary>
    /// ازکارافتاده
    /// </summary>
    Disabled = 13,
    /// <summary>
    /// فرزند خوانده
    /// </summary>
    AdoptedChild = 14,
    /// <summary>
    /// طلاق
    /// </summary>
    Divorce = 15,
    /// <summary>
    /// بيمه جاي ديگر
    /// </summary>
    InsuranceElsewhere = 17,
    /// <summary>
    /// دانشجو
    /// </summary>
    UniversityStudent = 18,
    /// <summary>
    /// بدسرپرست
    /// </summary>
    Neglected = 19,
    /// <summary>
    /// بدون سرپرست
    /// </summary>
    Orphaned = 20,
    /// <summary>
    /// سرباز
    /// </summary>
    Soldier = 24,
    /// <summary>
    /// کد مستقل
    /// </summary>
    IndependentCode = 25,
    /// <summary>
    /// فوت همسر و انتقال
    /// </summary>
    DeceasedSpouseAndTransfer = 29,
    /// <summary>
    /// فوت همسر
    /// </summary>
    DeceasedSpouse = 32,
    /// <summary>
    /// عدم ازدواج
    /// </summary>
    Unmarried = 33,
    /// <summary>
    /// کمیسیون
    /// </summary>
    Commission = 36,
    /// <summary>
    /// متاهل
    /// </summary>
    Married = 38,
    /// <summary>
    /// تامين اجتماعي از طريق طلبه
    /// </summary>
    SocialSecurityThroughCleric = 41,
    /// <summary>
    /// تاييديه المصطفي
    /// </summary>
    AlMustafaCertification = 44
}
