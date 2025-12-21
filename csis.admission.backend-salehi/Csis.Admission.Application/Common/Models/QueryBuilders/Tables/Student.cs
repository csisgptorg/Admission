using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "اطلاعات اصلی", Name = "stu.StudentSummary",Tab =Enums.ReportBuilderTab.Student)]
public class Student : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "کد مرکز خدمات",Operators = ["=","in"])]
    public int Codm { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام")]
    public string FirstName { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "نام خانوادگی")]
    public string LastName { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "نام پدر")]
    public string FatherName { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "سید")]
    public bool IsSadat { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ تولد",Type =typeof(DateTime))]
    public int? BirthDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "جنسیت")]
    public Gender Gender { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تلفن همراه",Operators =["="])]
    public string Mobile { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "مذهب")]
    public Religion? Religion { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تابعیت")]
    public Citizenship? Citizenship { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "کد ملی")]
    public string NationalCode { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "شماره شناسنامه")]
    public string BirthCertNumber { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "سریال شناسنامه")]
    public int? BirthCertSerial { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "محل صدور")]
    public string BirthCertIssuePlace { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "استان محل صدور شناسنامه")]
    public string BirthCertIssueProvince { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "توضیحات شناسنامه")]
    public string BirthCertDescription { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "ملیت")]
    public ReportBuilderNationality? Nationality { get; set; }//TODO باید ای پی آی تبدیل شود
    /// <inheritdoc/>
    [QueryBuilder(Label = "شماره پاسپورت")]
    public string PassportNumber { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "کد فیدا")]
    public string FidaCode { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "کد یکتا")]
    public string YektaCode { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "متأهل")]
    public bool IsMarried { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ ازدواج", Type = typeof(DateTime))]
    public int? MarriageDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ طلاق", Type = typeof(DateTime))]
    public int? DivorceDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "نوع تجرد")]
    public SingleStatus? SingleStatus { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "مرحوم")]
    public bool IsDead { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ فوت", Type = typeof(DateTime))]
    public int? DeathDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "دارای پرونده فعال")]
    public bool IsActive { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "شعبه", RelationTable = nameof(Branch), Source = Enums.ColumnSourceType.Api)]
    public int? BranchId { get; set; }
    /// /// <inheritdoc/>
    [QueryBuilder(Label = "نمایندگی",RelationTable =nameof(Agency),Source =Enums.ColumnSourceType.Api, DependentApi = "/private/branches?hasAgency=true", DependentLabel="شعبه")]
    public int AgencyId { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "مرجع تایید کننده حوزوی")]
    public ApprovalCenter? ApprovalCenter { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "شماره پرونده در مرجع تایید کننده حوزوی")]
    public long? CaseNumInApprovalCenter { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "شماره حساب")]
    public string BankAccountNumber { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "مسدود")]
    public bool IsBlock { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ انسداد پرونده", Type = typeof(DateTime))]
    public int? BlockDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ تشکیل پرونده", Type = typeof(DateTime))]
    public int? CaseCreationDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ اعتبار پرونده", Type = typeof(DateTime))]
    public int? CaseValidityDate { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "تراز تحصیلی")]
    public int? Taraz { get; set; }
    /// <inheritdoc/>
    [QueryBuilder(Label = "توضیحات پرونده")]
    public string CaseDescription { get; set; }
}
