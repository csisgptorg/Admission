using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <inheritdoc/>
public record StudentInfoDto : BaseDto<StudentInfoDto, StudentInfo>
{
    /// <inheritdoc/>
    public int Codm { get; init; }

    /// <inheritdoc/>
    public string FirstName { get; init; }

    /// <inheritdoc/>
    public bool? IsSadat { get; init; }

    /// <inheritdoc/>
    public string LastName { get; init; }

    /// <inheritdoc/>
    public string FatherName { get; init; }

    /// <inheritdoc/>
    public string BirthDate { get; init; }


    public string GregorianBirthDate { get; init; }

    /// <inheritdoc/>
    public Gender? Gender { get; init; }

    /// <summary>مذهب</summary>
    public Religion? Religion { get; init; }

    /// <summary>تابعیت</summary>
    public Citizenship? Citizenship { get; init; }

    /// <inheritdoc/>
    public string NationalCode { get; init; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    public string BirthCertNumber { get; init; }

    /// <summary>
    /// سری شناسنامه
    /// </summary>
    public string BirthCertSeri { get; init; }

    /// <summary>
    /// سریال شناسنامه
    /// </summary>
    public int? BirthCertSerial { get; init; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssuePlace { get; init; }

    /// <summary>
    /// استان محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssueProvince { get; init; }

    /// <summary>
    /// توضیحات شناسنامه
    /// </summary>
    public string BirthCertDescription { get; init; }

    /// <summary>
    /// ملیت
    /// </summary>
    public short? Nationality { get; init; }

    /// <summary>
    /// ملیت
    /// </summary>
    public string NationalityTitle { get; init; }

    /// <inheritdoc/>
    public string PassportNumber { get; init; }

    /// <inheritdoc/>
    public string FidaCode { get; init; }

    /// <inheritdoc/>
    public string YektaCode { get; init; }

    /// <summary>
    /// تاریخ اعتبار اقامت
    /// </summary>
    public string ResidenceExpireDate { get; init; }

    /// <inheritdoc/>
    public bool IsMarried { get; init; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }

    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <summary>
    /// وضعیت تجرد
    /// </summary>
    public SingleStatus? SingleStatus { get; init; }

    /// <inheritdoc/>
    public bool IsDead { get; init; }

    /// <inheritdoc/>
    public string DeathDate { get; init; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<StudentInfo, StudentInfoDto> mapping) {
        mapping.ForMember(dto => dto.BirthDate, config => config.MapFrom(model => model.BirthDate.IntDateToString()));
        mapping.ForMember(dto => dto.ResidenceExpireDate, config => config.MapFrom(model => model.ResidenceExpireDate.IntDateToString()));
        mapping.ForMember(dto => dto.MarriageDate, config => config.MapFrom(model => model.MarriageDate.IntDateToString()));
        mapping.ForMember(dto => dto.DivorceDate, config => config.MapFrom(model => model.DivorceDate.IntDateToString()));
        mapping.ForMember(dto => dto.DeathDate, config => config.MapFrom(model => model.DeathDate.IntDateToString()));
        mapping.ForMember(dto => dto.GregorianBirthDate, config => config.MapFrom(model => model.Citizenship == Domain.Enums.Citizenship.NonIranian? model.BirthDate.IntDateToGregorianStingDate() : null));
    }
}
