using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.People.Dtos;

/// <summary>
/// مدل نمایشی موجودیت شخص
/// </summary>
public sealed record PersonDto : BaseDto<PersonDto, Person>
{
    /// <summary>
    /// شماره حساب
    /// </summary>
    public string BankAccountNumber { get; init; }

    /// <summary>
    /// شماره شبا
    /// </summary>
    public string ShebaNumber { get; init; }

    /// <summary>
    /// توضیحات شناسنامه
    /// </summary>
    public string BirthCertDescription { get; init; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssuePlace { get; init; }

    /// <summary>
    /// استان محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssueProvince { get; init; }

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
    public string BirthCertSerial { get; init; }

    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; init; }

    /// <summary>
    /// نام مادر
    /// </summary>
    public string MotherName { get; init; }

    /// <summary>
    /// شناسه فیدا
    /// </summary>
    public string FidaCode { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// تلفن همراه
    /// </summary>
    public string Mobile { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }

    /// <summary>
    /// شماره پاسپورت
    /// </summary>
    public string PassportNumber { get; init; }

    /// <summary>
    /// شناسه یکتا
    /// </summary>
    public string YektaCode { get; init; }

    /// <summary>
    /// ملیت
    /// </summary>
    public short Nationality { get; init; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public string? BirthDate { get; init; }

    /// <summary>
    /// مرحوم است
    /// </summary>
    public bool IsDead { get; init; }

    /// <summary>
    /// سیادت
    /// </summary>
    public bool IsSadat { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public string? DeathDate { get; init; }

    /// <summary>
    /// علت فوت
    /// </summary>
    public DeathCause? DeathCause { get; init; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// مذهب
    /// </summary>
    public Religion Religion { get; init; }

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship Citizenship { get; init; }

    /// <summary>
    /// شناسه تصویر شخص
    /// </summary>
    public Guid? PersonImage { get; init; }

    /// <summary>
    /// کد یکتای منحصر به فرد 
    /// </summary>
    public int UniqueCode { get; init; }

    /// <summary>
    /// نوع ایجاد شخص
    /// </summary>
    public PersonCreateType? CreateType { get; init; }


    /// <summary>
    /// لیست روابط شخص
    /// </summary>
    public List<PersonRelationsInfoDto> Relations { get; init; } = new();

  

    public override void CustomMappings(IMappingExpression<Person, PersonDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(x => x.BirthDate, opt => opt.MapFrom(x => x.BirthDate.HasValue ? x.BirthDate.Value.ToPersianDateTime().ToString() : null))
            .ForMember(x => x.DeathDate, opt => opt.MapFrom(x => x.DeathDate.HasValue ? x.DeathDate.Value.ToPersianDateTime().ToString() : null));

    }
}
