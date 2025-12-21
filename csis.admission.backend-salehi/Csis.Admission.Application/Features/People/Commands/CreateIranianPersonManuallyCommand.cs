using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// ثبت دستی فرد ایرانی
/// </summary>
public sealed record CreateIranianPersonManuallyCommand : BaseCommandDto<CreateIranianPersonManuallyCommand, Person>, IRequest<int>
{
    /// <summary> محل صدور شناسنامه </summary>
    public string BirthCertIssuePlace { get; init; }

    /// <summary> استان محل صدور شناسنامه </summary>
    public string BirthCertIssueProvince { get; init; }

    /// <summary> شماره شناسنامه </summary>
    public string BirthCertNumber { get; init; }

    /// <summary> سری شناسنامه </summary>
    public string BirthCertSeri { get; init; }

    /// <summary> سریال شناسنامه </summary>
    public string BirthCertSerial { get; init; }

    /// <summary> نام </summary>
    public string FirstName { get; init; }

    /// <summary> نام خانوادگی </summary>
    public string LastName { get; init; }

    /// <summary> کد ملی </summary>
    public string NationalCode { get; init; }

    /// <summary> تاریخ تولد </summary>
    public string? BirthDate { get; init; }

    /// <summary> شماره حساب </summary>
    public string BankAccountNumber { get; init; }

    /// <summary> شماره شبا </summary>
    public string ShebaNumber { get; init; }

    /// <summary> توضیحات شناسنامه </summary>
    public string BirthCertDescription { get; init; }

    /// <summary> نام پدر </summary>
    public string FatherName { get; init; }

    /// <summary> نام مادر </summary>
    public string MotherName { get; init; }

    /// <summary> تلفن همراه </summary>
    public string Mobile { get; init; }

    /// <summary> مرحوم است </summary>
    public bool IsDead { get; init; }

    /// <summary> سیادت </summary>
    public bool IsSadat { get; init; }

    /// <summary> تاریخ فوت </summary>
    public string? DeathDate { get; init; }

    /// <summary> علت فوت </summary>
    public DeathCause? DeathCause { get; init; }

    /// <summary> جنسیت </summary>
    public PersonEntityGender Gender { get; init; }

    /// <summary> مذهب </summary>
    public Religion Religion { get; init; }

    /// <summary> وضعیت تجرد </summary>
    public SingleStatus? SingleStatus { get; init; }

    /// <summary> شناسه تصویر شخص </summary>
    public Guid? PersonImage { get; init; }

    /// <summary> سفارشی سازی نگاشت </summary>
    public override void ReverseCustomMappings(IMappingExpression<CreateIranianPersonManuallyCommand, Person> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate.StringDateToInt()));
        mapping.ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => (short?) Nationality.Iranian));
        mapping.ForMember(dest => dest.Citizenship, opt => opt.MapFrom(src => Citizenship.Iranian));
    }
}
internal sealed class CreateIranianPersonManuallyCommandHandler(ISettingRepository settingRepository, IPersonRepository personRepo, ICsisWsmService csisWsmService) : IRequestHandler<CreateIranianPersonManuallyCommand, int>
{
    public async Task<int> Handle(CreateIranianPersonManuallyCommand request, CancellationToken cancellationToken) {

        await Common.Utilities.ValidateSettings(settingRepository, WebServiceSettingTitle.Iranian, RegistrationType.Manual, cancellationToken);

        if ( await personRepo.ExistsAsync(x => x.NationalCode == request.NationalCode && !string.IsNullOrEmpty(x.NationalCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.NationalCode), "کد ملی وارد شده تکراری است");
        }

        var person = request.ToEntity();
        person.CreateType = PersonCreateType.ManualByUser;
        person.UniqueCode = await personRepo.GetNextYektaCodeAsync();

        if ( request.BankAccountNumber.HasValue() ) {
            person = await InsertSheba(person, cancellationToken);
        }

        await personRepo.InsertAsync(person, cancellationToken: cancellationToken);
        return person.Id;
    }

    private async Task<Person> InsertSheba(Person person, CancellationToken cancellationToken) {
        var existingSheba = await personRepo.ExistsAsync(
            p => p.BankAccountNumber == person.BankAccountNumber,
            cancellationToken: cancellationToken);

        if ( existingSheba ) {
            throw new CommandValidationException("شماره شبا وارد شده قبلا ثبت شده است.");
        }

        person.ShebaNumber = person.ShebaNumber;
        person.BankAccountNumber = person.BankAccountNumber;

        return person;
    }
}
