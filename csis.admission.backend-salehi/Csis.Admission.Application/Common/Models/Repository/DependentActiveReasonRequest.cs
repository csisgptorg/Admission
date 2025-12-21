namespace Csis.Admission.Application.Common;

public class DependentActiveReasonRequest
{// ورودی های محاسبه علت فعال یا غیر فعال بودن تکفل

    /// <summary>
    /// نسبت تکفل
    /// </summary>
    public short? DependentRelation { get; set; }

    /// <summary>
    /// سن تکفل
    /// </summary>
    public double DependentAge { get; set; }

    /// <summary>
    /// آیا تکفل مرحوم است؟
    /// </summary>
    public bool DependentIsDead { get; set; }

    /// <summary>
    /// جنسیت تکفل
    /// </summary>
    public short DependentGender { get; set; }

    /// <summary>
    /// آیا تکفل ازدواج کرده است؟
    /// </summary>
    public bool DependentIsMarried { get; set; }

    /// <summary>
    /// وضعیت تاهل تکفل
    /// </summary>
    public short? DependentSingleStatus { get; set; }


    /// <summary>
    /// جنسیت دانشجو
    /// </summary>
    public short? StudentGender { get; set; }

    /// <summary>
    /// آیا تکفل فعال است؟
    /// </summary>
    public bool? DependentIsActive { get; set; }

    /// <summary>
    /// علت فعال بودن تکفل
    /// </summary>
    public short? DependentActiveReason { get; set; }

    /// <summary>
    /// علت غیر فعال بودن تکفل
    /// </summary>
    public short? DependentDeActiveReason { get; set; }

    /// <summary>
    /// علت غیر فعال بودن تکفل در صورت انقضاء
    /// </summary>
    public short? DependentDeActiveReasonOnExpire { get; set; }

    /// <summary>
    /// تاریخ انقضای تکفل
    /// </summary>
    public int? DependentExpireDate { get; set; }

    /// <summary>
    /// آیا تکفل در حال تحصیل است؟
    /// </summary>
    public bool IsDependentInStudy { get; set; }

    /// <summary>
    /// تاریخ اعتبار تکفل در حال تحصیل
    /// </summary>
    public int? DependentInStudyValidityDate { get; set; }

    /// <summary>
    /// آیا تکفل از کار افتاده است؟
    /// </summary>
    public bool IsDependentAzkaroftade { get; set; }

    /// <summary>
    /// تاریخ اعتبار تکفل از کار افتاده
    /// </summary>
    public int? DependentAzkaroftadeValidityDate { get; set; }

    /// <summary>
    /// آیا تکفل شاغل است؟
    /// </summary>
    public bool IsDependentEmployed { get; set; }

    /// <summary>
    /// آیا تکفل دارای کمیسیون معتبر است؟
    /// </summary>
    public bool HasValidDependentCommission { get; set; }

    /// <summary>
    /// تاریخ اعتبار تکفل دارای کمیسیون
    /// </summary>
    public int? DependentCommissionValidityDate { get; set; }


    /// <summary>
    /// آیا دانشجو دارای بیمه تامین اجتماعی فعال است؟
    /// </summary>
    public bool HasActiveTaminInsurance { get; set; }

    /// <summary>
    /// آیا دانشجو دارای پرونده فعال است؟
    /// </summary>
    public bool HasActiveStudentCase { get; set; }

    /// <summary>
    /// آیا دانشجو در وضعیت انسداد است؟
    /// </summary>
    public bool IsStudentBlock { get; set; }

    /// <summary>
    /// آیا تکفل به کد مستقل منتقل شده است؟
    /// </summary>
    public int? DependentTransferredToCodm { get; set; }
}


/// <summary>
/// خروجی های محاسبه علت فعال یا غیر فعال بودن تکفل
/// </summary>
public class DependentActiveDeactiveReason
{
    /// <summary>
    /// آیا تکفل فعال است؟
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// علت فعال یا غیر فعال بودن تکفل
    /// </summary>
    public short? ActiveReason { get; set; }

    /// <summary>
    /// علت غیر فعال بودن تکفل
    /// </summary>
    public short? DeActiveReason { get; set; }

    /// <summary>
    /// علت غیر فعال بودن تکفل در صورت انقضاء
    /// </summary>
    public short? DeActiveReasonOnExpire { get; set; }

    /// <summary>
    /// تاریخ انقضای تکفل
    /// </summary>
    public int? ExpireDate { get; set; }
}
