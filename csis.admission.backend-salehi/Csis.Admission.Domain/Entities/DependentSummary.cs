using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;
using System.Reflection;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// مشخصات تکفل ها
/// </summary>
public class DependentSummary : BaseEntity<long>, IFilterable
{
    /// <summary>
    /// کد مرکز سرپرست
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>نام کامل</summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// وضعیت سید بودن
    /// </summary>
    public bool? IsSadat { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; set; }

    /// <summary>
    /// نام مادر
    /// </summary>
    public string MotherName { get; set; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public int? BirthDate { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// مذهب
    /// </summary>
    public Religion? Religion { get; set; }

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship? Citizenship { get; set; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; set; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    public string BirthCertNumber { get; set; }

    /// <summary>
    /// سری شناسنامه
    /// </summary>
    public string BirthCertSeri { get; set; }

    /// <summary>
    /// سریال شناسنامه
    /// </summary>
    public int? BirthCertSerial { get; set; }

    /// <summary>
    /// محل صدور
    /// </summary>
    public string BirthCertIssuePlace { get; set; }

    /// <summary>
    /// توضیحات شناسنامه
    /// </summary>
    public string BirthCertDescription { get; set; }

    /// <summary>
    /// ملیت / نام کشورها
    /// </summary>
    public Nationality? Nationality { get; set; }



    /// <summary>
    /// شماره گذرنامه
    /// </summary>
    public string PassportNumber { get; set; }

    /// <summary>
    /// کد فیدا
    /// </summary>
    public string FidaCode { get; set; }

    /// <summary>
    /// کد یکتا
    /// </summary>
    public string YektaCode { get; set; }

    /// <summary>
    /// تاریخ انقضای اقامت
    /// </summary>
    public int? ResidenceExpireDate { get; set; }

    /// <summary>
    /// وضعیت تأهل
    /// </summary>
    public bool IsMarried { get; set; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public int? MarriageDate { get; set; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public int? DivorceDate { get; set; }

    /// <summary>
    /// وضعیت تجرد
    /// </summary>
    public SingleStatus? SingleStatus { get; set; }

    /// <summary>
    /// وضعیت فوت
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public int? DeathDate { get; set; }

    /// <summary>نسبت</summary>
    public DependentRelation? Relation { get; set; }

    /// <summary>عناوین نسبت - متفاوت از اینام و شخصی سازی شده</summary>
    public short? RelationOrder { get; set; }

    /// <summary>
    /// فعال بودن پرونده
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public int? CaseCreateDate { get; set; }

    /// <summary>
    /// تاریخ غیرفعال شدن پرونده
    /// </summary>
    public int? CaseDeactiveDate { get; set; }



    /// <summary>
    /// توضیحات پرونده
    /// </summary>
    public string CaseDescription { get; set; }

    /// <summary>
    /// کد مستقل
    /// </summary>
    public int? IndependentCodm { get; set; }

    /// <summary>
    /// کد انتقال
    /// </summary>
    public int? TransferredToCodm { get; set; }

    /// <summary>علت فعال بودن</summary>
    public DependentActiveReasonEnum? ActiveReason { get; set; }

    /// <summary>علت غیر فعال بودن</summary>
    public DependentDeActiveReasonEnum? DeActiveReason { get; set; }

    /// <summary>شماره حساب</summary>
    public string BankAccountNumber { get; set; }

    /// <summary>موبایل</summary>
    public string Mobile { get; set; }

    /// <summary>شناسه شعبه</summary>
    public short? BranchId { get; set; }

    /// <summary>خدمات مسدود</summary>
    public ICollection<DependentBlockService> BlockServices { get; set; }

    /// <summary>علت غیرفعال شدن خودکار پرونده هنگام انقضا</summary>
    public short? DeActiveReasonOnExpire { get; set; }
    /// <summary>تاریخ انقضای پرونده</summary>
    public int? DateExpire { get; set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [
            nameof(Codm),
            nameof(NationalCode),
            nameof(FirstName),
            nameof(LastName),
            nameof(FatherName),
            nameof(YektaCode),
            nameof(NationalCode),
            nameof(FidaCode),
            nameof(PassportNumber),
            nameof(BirthCertNumber),
            nameof(BranchId),
            nameof(Mobile),
            nameof(BankAccountNumber),
            nameof(CaseDescription),
            nameof(Citizenship),
            nameof(Religion),
            nameof(Gender),
            nameof(IsMarried),
            nameof(IsDead)];
    }
}
