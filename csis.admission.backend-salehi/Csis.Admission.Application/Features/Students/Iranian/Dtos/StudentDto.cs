using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

///<inheritdoc/>
public sealed record StudentDto : BaseDto<StudentDto, Student>
{
    ///<inheritdoc/>
    public int Codm { get; set; }

    ///<inheritdoc/>
    public string FirstName { get; set; }

    ///<inheritdoc/>
    public string LastName { get; set; }

    ///<inheritdoc/>
    public string FatherName { get; set; }

    ///<inheritdoc/>
    public bool? IsSadat { get; set; }

    ///<inheritdoc/>
    public string BirthDate { get; set; }

    /// <summary>تاریخ تولد میلادی</summary>
    public string GregorianBirthDate { get; set; }

    ///<inheritdoc/>
    public Gender? Gender { get; set; }

    ///<inheritdoc/>
    public Religion? Religion { get; set; }

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship? Citizenship { get; set; }

    ///<inheritdoc/>
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
    /// محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssuePlace { get; set; }

    /// <summary>
    /// استان محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssueProvince { get; set; }

    /// <summary>
    /// توضیحات شناسنامه
    /// </summary>
    public string BirthCertDescription { get; set; }

    /// <summary>
    /// Nationality
    /// </summary>
    public Nationality? Nationality { get; set; }

    /// <summary>
    /// PassportNumber
    /// </summary>
    public string PassportNumber { get; set; }

    /// <summary>
    /// FidaCode
    /// </summary>
    public string FidaCode { get; set; }

    /// <summary>
    /// YektaCode
    /// </summary>
    public string YektaCode { get; set; }

    /// <summary>
    /// تاریخ اعتبار اقامت
    /// </summary>
    public string ResidenceExpireDate { get; set; }

    /// <summary>
    /// IsMarried
    /// </summary>
    public bool IsMarried { get; set; }

    /// <summary>
    /// وضعیت تجرد
    /// </summary>
    public SingleStatus? SingleStatus { get; set; }

    /// <summary>
    /// IsDead
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// DeathDate
    /// </summary>
    public string DeathDate { get; set; }

    /// <summary>
    /// طلبه
    /// </summary>
    public bool IsStudent { get; set; }

    /// <summary>
    /// مسدود
    /// </summary>
    public bool IsBlock { get; set; }

    /// <summary>
    /// تاریخ انسداد
    /// </summary>
    public string BlockDate { get; set; }

    /// <summary>
    /// وضعیت پرونده
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// تاریخ اعتبار پرونده
    /// </summary>
    public string CaseValidityDate { get; set; }

    /// <summary>
    /// تاریخ تشیکل پرونده
    /// </summary>
    public string CaseCreationDate { get; set; }

    /// <summary>
    /// علت تمدید پرونده
    /// </summary>
    public string ValidityExtensionReasonTitle { get; set; }

    /// <summary>
    /// علت انسداد پرونده
    /// </summary>
    public string BlockReasonTitle { get; set; }

    /// <summary>
    /// توضیحات پرونده
    /// </summary>
    public string CaseDescription { get; set; }

    /// <summary>
    /// مرجع تایید کنننده حوزوی
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// شماره پرونده
    /// </summary>
    public long? CaseNumInApprovalCenter { get; set; }

    /// <summary>
    /// نوع کمیسیون
    /// </summary>
    public CommissionType? CommissionRequestId { get; set; }

    /// <summary>
    /// تراز تحصیلی
    /// </summary>
    public short? Taraz { get; set; }

    /// <summary>
    /// تاریخ تلبس
    /// </summary>
    public string ReligiouslyDressedDate { get; set; }

    /// <summary>
    /// شاغل
    /// </summary>
    public bool IsEmployment { get; set; }

    /// <summary>
    /// دارای درآمد مکفی
    /// </summary>
    public bool HasEnoughIncome { get; set; }

    /// <summary>
    /// شهید
    /// </summary>
    public bool IsMartyr { get; set; }

    /// <summary>
    /// نوع مشهور بودن
    /// </summary>
    public FamousType? FamousType { get; set; }

    /// <summary>
    /// زندگی در مناطق اقلیت شیعه
    /// </summary>
    public bool LiveInAghaliatShiaPlace { get; set; }

    /// <summary>زندگی در مناطق محروم</summary>
    public bool LiveInPoorPlace { get; set; }

    /// <summary>نخبه</summary>
    public bool IsElite { get; set; }

    /// <summary>سال ورود به حوزه</summary>
    public int EnteringYear { get; set; }

    /// <inheritdoc/>
    public EducationStatus EducationStatus { get; set; }

    /// <inheritdoc/>
    public string NationalityTitle { get; set; }

    /// <inheritdoc/>
    public string MarriageDate { get; set; }

    /// <inheritdoc/>
    public string DivorceDate { get; set; }

    /// <summary>شعبه</summary>
    public string BranchTitle { get; set; }

    /// <summary>طلبه چند همسر دارد</summary>
    public bool HasSeveralSurvivingWife { get; set; }

    /// <summary>کد مرکز خدمات همسران طلبه متوفی</summary>
    public int[] SeveralSurvivingWifeCodms { get; set; }

    ///<inheritdoc/>
    public bool IsTenant { get; set; }
    ///<inheritdoc/>
    public bool IsFamous { get; set; }
    ///<inheritdoc/>
    public string Age { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Student, StudentDto> mapping) {
        mapping.ForMember(dto => dto.BirthDate, config => config.MapFrom(model => model.BirthDate.IntDateToString()));
        mapping.ForMember(dto => dto.GregorianBirthDate, config => config.MapFrom(model => model.BirthDate‌.ToString().StartsWith("13") || BirthDate.StartsWith("14") ? model.BirthDate.IntDateToGregorianStingDate() : null));
        mapping.ForMember(dto => dto.Age, config => config.MapFrom(model => Common.Utilities.CalculateAgeDetailed(model.BirthDate,model.DeathDate)));
        mapping.ForMember(dto => dto.ResidenceExpireDate, config => config.MapFrom(model => model.ResidenceExpireDate.IntDateToString()));
        mapping.ForMember(dto => dto.DeathDate, config => config.MapFrom(model => model.DeathDate.IntDateToString()));
        mapping.ForMember(dto => dto.BlockDate, config => config.MapFrom(model => model.BlockDate.IntDateToString()));
        mapping.ForMember(dto => dto.CaseValidityDate, config => config.MapFrom(model => model.CaseValidityDate.IntDateToString()));
        mapping.ForMember(dto => dto.CaseCreationDate, config => config.MapFrom(model => model.CaseCreationDate.IntDateToString()));
        mapping.ForMember(dto => dto.ReligiouslyDressedDate, config => config.MapFrom(model => model.ReligiouslyDressedDate.IntDateToString()));
        mapping.ForMember(dto => dto.MarriageDate, config => config.MapFrom(model => model.MarriageDate.IntDateToString()));
        mapping.ForMember(dto => dto.DivorceDate, config => config.MapFrom(model => model.DivorceDate.IntDateToString()));
    }
}
