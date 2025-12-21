using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "اطلاعات اصلی", Name = "stu.DependentSummary",Tab = Enums.ReportBuilderTab.Dependent)]
public class Dependent : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "شناسه تکفل",Name ="Id")]
    public long Id { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام")]
    public string FirstName { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام خانوادگی")]
    public string LastName { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "سید")]
    public bool? IsSadat { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام پدر")]
    public string FatherName { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام مادر")]
    public string MotherName { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ تولد", Type = typeof(DateTime))]
    public int? BirthDate { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "جنسیت")]
    public Gender Gender { get; set; }

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
    [QueryBuilder(Label = "سری شناسنامه")]
    public string BirthCertSeri { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "سریال شناسنامه")]
    public int? BirthCertSerial { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "محل صدور")]
    public string BirthCertIssuePlace { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "توضیحات شناسنامه")]
    public string BirthCertDescription { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "ملیت")]
    public ReportBuilderNationality? Nationality { get; set; }

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
    [QueryBuilder(Label = "تاریخ انقضای اقامت", Type = typeof(DateTime))]
    public int? ResidenceExpireDate { get; set; }

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
    [QueryBuilder(Label = "وضعیت تجرد")]
    public SingleStatus? SingleStatus { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "مرحوم")]
    public bool IsDead { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تاریخ فوت", Type = typeof(DateTime))]
    public int? DeathDate { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نسبت")]
    public DependentRelation? Relation { get; set; }
}
