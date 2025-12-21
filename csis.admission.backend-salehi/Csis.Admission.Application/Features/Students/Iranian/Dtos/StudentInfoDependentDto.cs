using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// اطلاعات شناسنامه تکفل طلبه
/// </summary>
public record StudentInfoDependentDto : BaseDto<StudentInfoDependentDto, StudentDependent,long>
{
    /// <summary>
    /// کد مرکز سرپرست
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// وضعیت سید بودن
    /// </summary>
    public bool IsSadat { get; set; }

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
    public string BirthDate { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender? Gender { get; set; } // enum

    /// <summary>
    /// مذهب
    /// </summary>
    public Religion? Religion { get; set; }// enum

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship? Citizenship { get; set; }// enum

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
    public int? Nationality { get; set; }

    /// <summary>
    /// عنوان ملیت
    /// </summary>
    public string NationalityTitle { get; set; }

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
    public string ResidenceExpireDate { get; set; }

    /// <summary>
    /// وضعیت تأهل
    /// </summary>
    public bool IsMarried { get; set; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public string MarriageDate { get; set; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public string DivorceDate { get; set; }

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
    public string DeathDate { get; set; }

    /// <summary>
    /// نسبت
    /// </summary>
    public string RelationTitle { get; set; }

    /// <summary>
    /// علت باز یا بسته بودن پرونده
    /// </summary>
    public CaseBlockReason? StatusReason { get; set; }

    /// <summary>
    /// کد مستقل
    /// </summary>
    public int? IndependentCodm { get; set; }


    /// <summary>
    /// کد انتقال
    /// </summary>
    public int? TransferredToCodm { get; set; }

    //TODO داینامیک و ساختارمند شود
    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<StudentDependent, StudentInfoDependentDto> mapping) {
        mapping.ForMember(dto => dto.BirthDate, config => config.MapFrom(model => model.BirthDate.IntDateToString()));
        mapping.ForMember(dto => dto.ResidenceExpireDate, config => config.MapFrom(model => model.ResidenceExpireDate.IntDateToString()));
        mapping.ForMember(dto => dto.MarriageDate, config => config.MapFrom(model => model.MarriageDate.IntDateToString()));
        mapping.ForMember(dto => dto.DivorceDate, config => config.MapFrom(model => model.DivorceDate.IntDateToString()));
        mapping.ForMember(dto => dto.DeathDate, config => config.MapFrom(model => model.DeathDate.IntDateToString()));
    }
}
