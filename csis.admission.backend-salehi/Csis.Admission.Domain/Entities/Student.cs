using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class Student : SoftDeletedBaseEntity, IAuditable
{
    /// <inheritdoc/>
    public int? Codm { get; set; }

    /// <inheritdoc/>
    public string? Mobile { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string? LastName { get; set; }

    /// <inheritdoc/>
    public string FatherName { get; set; }

    /// <inheritdoc/>
    public bool IsSadat { get; set; }

    /// <inheritdoc/>
    public int? BirthDate { get; set; }

    /// <inheritdoc/>
    public Gender Gender { get; set; }

    /// <inheritdoc/>
    public Religion Religion { get; set; }

    /// <summary>تابعیت</summary>
    public Citizenship Citizenship { get; set; }

    /// <inheritdoc/>
    public string? NationalCode { get; set; }

    /// <summary>شماره شناسنامه</summary>
    public string? BirthCertNumber { get; set; }

    /// <summary>سری شناسنامه</summary>
    public string? BirthCertSeri { get; set; }

    /// <summary>سریال شناسنامه</summary>
    public int? BirthCertSerial { get; set; }

    /// <summary>محل صدور شناسنامه</summary>
    public string? BirthCertIssuePlace { get; set; }

    /// <summary>استان محل صدور شناسنامه</summary>
    public string? BirthCertIssueProvince { get; set; }

    /// <summary>توضیحات شناسنامه</summary>
    public string? BirthCertDescription { get; set; }

    /// <inheritdoc/>
    public Nationality? Nationality { get; set; }

    /// <inheritdoc/>
    public string? PassportNumber { get; set; }

    /// <inheritdoc/>
    public string? FidaCode { get; set; }

    /// <inheritdoc/>
    public string? YektaCode { get; set; }

    /// <summary>تاریخ اعتبار اقامت</summary>
    public int? ResidenceExpireDate { get; set; }

    /// <inheritdoc/>
    public bool IsMarried { get; set; }

    /// <summary>وضعیت تجرد</summary>
    public SingleStatus? SingleStatus { get; set; }

    /// <inheritdoc/>
    public bool IsDead { get; set; }

    /// <inheritdoc/>
    public int? DeathDate { get; set; }

    /// <summary>طلبه</summary>
    public bool IsStudent { get; set; }

    /// <summary>مسدود</summary>
    public bool IsBlock { get; set; }

    /// <summary>تاریخ انسداد</summary>
    public int? BlockDate { get; set; }

    /// <summary>وضعیت پرونده</summary>
    public bool IsActive { get; set; }

    /// <summary>تاریخ اعتبار پرونده</summary>
    public int CaseValidityDate { get; set; }

    /// <summary>تاریخ تشکیل پرونده</summary>
    public int CaseCreationDate { get; set; }

    /// <summary>علت تمدید پرونده</summary>
    public string? ValidityExtensionReasonTitle { get; set; }

    /// <summary>علت انسداد پرونده</summary>
    public string? BlockReasonTitle { get; set; }

    /// <summary>توضیحات پرونده</summary>
    public string? CaseDescription { get; set; }

    /// <summary>مرجع تایید کننده حوزوی</summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>شماره پرونده</summary>
    public long? CaseNumInApprovalCenter { get; set; }

    /// <summary>نوع کمیسیون</summary>
    public CommissionType? CommissionRequestId { get; set; }

    /// <summary>تراز تحصیلی</summary>
    public short? Taraz { get; set; }

    /// <summary>تاریخ تلبس</summary>
    public int? ReligiouslyDressedDate { get; set; }

    /// <summary>شاغل</summary>
    public bool? IsEmployment { get; set; }

    /// <summary>دارای درآمد مکفی</summary>
    public bool? HasEnoughIncome { get; set; }

    /// <summary>شهید</summary>
    public bool? IsMartyr { get; set; }

    /// <summary>نوع مشهور بودن</summary>
    public FamousType? FamousType { get; set; }

    /// <summary>زندگی در مناطق اقلیت شیعه</summary>
    public bool? LiveInAghaliatShiaPlace { get; set; }

    /// <summary>زندگی در مناطق محروم</summary>
    public bool? LiveInPoorPlace { get; set; }

    /// <summary>نخبه</summary>
    public bool? IsElite { get; set; }

    /// <summary>سال ورود به حوزه</summary>
    public int? EnteringYear { get; set; }

    /// <summary>وضعیت تحصیلی</summary>
    public EducationStatus? EducationStatus { get; set; }

    /// <summary>عنوان ملیت</summary>
    public string? NationalityTitle { get; set; }

    /// <summary>تاریخ ازدواج</summary>
    public int? MarriageDate { get; set; }

    /// <summary>تاریخ طلاق</summary>
    public int? DivorceDate { get; set; }

    /// <summary>شعبه</summary>
    public string BranchTitle { get; set; }

    /// <summary>طلبه چند همسر دارد</summary>
    public bool? HasSeveralSurvivingWife { get; set; }

    /// <inheritdoc/>
    public bool? IsTenant { get; set; }

    /// <inheritdoc/>
    public bool? IsFamous { get; set; }

    /// <summary>شناسه موقت</summary>
    public Guid? TempId { get; set; }

    /// <summary>منبع دیتا</summary>
    public DataSource? AuditDataSource { get; set; }

    /// <summary>شناسه درخواست</summary>
    public int? AuditRequestId { get; set; }

    /// <summary>شناسه پرسنل</summary>
    public int? AuditPersonId { get; set; }
}
