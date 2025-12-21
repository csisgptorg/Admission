using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class StudentSummary : BaseEntity, IFilterable
{
    /// <inheritdoc/>
    public int Codm { get; set; }
    /// <inheritdoc/>
    public string FirstName { get; set; }
    /// <inheritdoc/>
    public bool IsSadat { get; set; }
    /// <inheritdoc/>
    public string LastName { get; set; }
    /// <inheritdoc/>
    public string FatherName { get; set; }
    /// <inheritdoc/>
    public int? BirthDate { get; set; }
    /// <inheritdoc/>
    public Gender Gender { get; set; }
    /// <inheritdoc/>
    public string Mobile { get; set; }
    /// <inheritdoc/>
    public Religion? Religion { get; set; }
    /// <inheritdoc/>
    public Citizenship? Citizenship { get; set; }
    /// <inheritdoc/>
    public string NationalCode { get; set; }
    /// <inheritdoc/>
    public string BirthCertNumber { get; set; }
    /// <inheritdoc/>
    public string BirthCertSeri { get; set; }
    /// <inheritdoc/>
    public int? BirthCertSerial { get; set; }
    /// <inheritdoc/>
    public string BirthCertIssuePlace { get; set; }
    /// <inheritdoc/>
    public string BirthCertIssueProvince { get; set; }
    /// <inheritdoc/>
    public string BirthCertDescription { get; set; }
    /// <inheritdoc/>
    public short? Nationality { get; set; }
    /// <inheritdoc/>
    public string PassportNumber { get; set; }
    /// <inheritdoc/>
    public string FidaCode { get; set; }
    /// <inheritdoc/>
    public string YektaCode { get; set; }
    /// <inheritdoc/>
    public int? ResidenceExpireDate { get; set; }
    /// <inheritdoc/>
    public bool IsMarried { get; set; }
    /// <inheritdoc/>
    public int? MarriageDate { get; set; }
    /// <inheritdoc/>
    public int? DivorceDate { get; set; }
    /// <inheritdoc/>
    public int? SingleStatus { get; set; }
    /// <inheritdoc/>
    public bool IsDead { get; set; }
    /// <inheritdoc/>
    public int? DeathDate { get; set; }
    /// <inheritdoc/>
    public bool IsActive { get; set; }

    /// <summary>شناسه شعبه</summary>
    public int? BranchId { get; set; }

    /// <summary>شناسه نمایندگی</summary>
    public int AgencyId { get; set; }
    /// <summary>شناسه مرجع تایید کننده حوزوی</summary>
    public ApprovalCenter? ApprovalCenter { get; set; }
    /// <summary>شماره پرونده</summary>
    public long? CaseNumInApprovalCenter { get; set; }
    /// <summary>شماره حساب</summary>
    public string BankAccountNumber { get; set; }
    /// <summary>تاریخ اعتبار پرونده</summary>
    public int? CaseValidityDate { get; set; }
    /// <summary>تاریخ ایجاد پرونده</summary>
    public int? CaseCreationDate { get; set; }
    /// <summary>تراز</summary>
    public int? Taraz { get; set; }
    /// <summary>توضیحات</summary>
    public string CaseDescription { get; set; }
    /// <summary>مسدودی</summary>
    public bool IsBlock { get; set; }
    /// <summary>تاریخ مسدودی</summary>
    public int? BlockDate { get; set; }
    /// <summary>نشان‌دهنده این که صاحب پرونده طلبه است یا خیر</summary>
    public bool IsStudent { get; set; }
    /// <summary>تاریخ لباس دینی پوشیدن</summary>
    public int? ReligiouslyDressedDate { get; set; }

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
            nameof(AgencyId),
            nameof(Mobile),
            nameof(ApprovalCenter),
            nameof(CaseNumInApprovalCenter),
            nameof(BankAccountNumber),
            nameof(CaseDescription),
            nameof(Citizenship),
            nameof(Religion),
            nameof(Gender),
            nameof(IsMarried),
            nameof(IsBlock),
            nameof(IsActive),
            nameof(IsDead)];
    }
}
