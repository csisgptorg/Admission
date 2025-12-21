using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// پرونده پذیرش طلبه
/// </summary>
public sealed class AdmissionCaseUser : SoftDeletedBaseEntity<Guid>, IFilterable
{
    /// <summary>کدملی</summary>
    public string NationalCode { get; set; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; set; }

    /// <summary>تاریخ تولد</summary>
    public int BirthDate { get; set; }

    /// <summary>موبایل</summary>
    public string Mobile { get; set; }

    /// <summary>تابعیت</summary>
    public Citizenship? Citizenship { get; set; }

    /// <summary>مرکز تأیید کننده حوزوی</summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>شماره پرونده تحصیلی</summary>
    public int? CaseNumInApprovalCenter { get; set; }

    /// <summary>کدپستی</summary>
    public long? PostalCode { get; set; }

    /// <summary>تأیید اطلاعات هویتی</summary>
    public bool ConfirmIdentityInformation { get; set; }

    /// <summary>
    /// سریال کارت ملی
    /// </summary>
    public string? NationalCardSerial { get; set; }

    /// <summary>
    /// تأیید شماره موبایل
    /// </summary>
    public bool ConfirmMobile { get; set; }

    /// <summary>
    /// مرحله فعلی
    /// </summary>
    public AdmissionCaseStep? CaseStep { get; set; }

    /// <summary> مذهب </summary>
    public Religion? Religion { get; set; }

    /// <summary>
    /// اطلاعات جمع‌آوری شده در بخش های مختلف
    /// </summary>
    public string Payloads { get; set; }
    /// <summary>
    /// کد مرکز موقت
    /// </summary>
    public int Codm { get; }

    /// <summary>
    /// شناسه درخواست
    /// </summary>
    public long? RequestId { get; set; }

    public string[] GetFilterableFields() => [nameof(NationalCode), nameof(YektaCode), nameof(Mobile), nameof(Citizenship), nameof(ApprovalCenter), nameof(CaseNumInApprovalCenter), nameof(PostalCode), nameof(Payloads)
    ];
}
