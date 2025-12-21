using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// (ثبت استعلامی فرد ایرانی (با وب سرویس
/// </summary>
public sealed record CreateIranianPersonByInquiryCommand : BaseCommandDto<CreateIranianPersonByInquiryCommand, Person>, IRequest<int>
{
    /// <summary> کد ملی </summary>
    public string NationalCode { get; init; }

    /// <summary> تاریخ تولد </summary>
    public string? BirthDate { get; init; }

    /// <summary> شماره حساب </summary>
    public string BankAccountNumber { get; init; }

    /// <summary> توضیحات شناسنامه </summary>
    public string? BirthCertDescription { get; init; }

    /// <summary> نام مادر </summary>
    public string MotherName { get; init; }

    /// <summary> تلفن همراه </summary>
    public string Mobile { get; init; }

    /// <summary> سیادت </summary>
    public bool IsSadat { get; init; }

    /// <summary> مذهب </summary>
    public Religion Religion { get; init; }

    /// <summary> شناسه تصویر شخص </summary>
    public Guid? PersonImage { get; init; }

    /// <summary> سفارشی سازی نگاشت </summary>
    public override void ReverseCustomMappings(IMappingExpression<CreateIranianPersonByInquiryCommand, Person> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.StringDateToInt()));
        mapping.ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => (short?) Nationality.Iranian));
        mapping.ForMember(dest => dest.Citizenship, opt => opt.MapFrom(src => Citizenship.Iranian));
    }
}
internal sealed class CreateIranianPersonByInquiryCommandHandler(ISettingRepository settingRepository, IPersonRepository personRepo, ICsisWsmService csisWsmService) : IRequestHandler<CreateIranianPersonByInquiryCommand, int>
{
    public async Task<int> Handle(CreateIranianPersonByInquiryCommand request, CancellationToken cancellationToken) {

        await Common.Utilities.ValidateSettings(settingRepository,WebServiceSettingTitle.Iranian,RegistrationType.Automatic,cancellationToken);

        if ( await personRepo.ExistsAsync(x => x.NationalCode == request.NationalCode && !string.IsNullOrEmpty(x.NationalCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.NationalCode), "کد ملی وارد شده تکراری است");
        }

        var result = await csisWsmService.GetIdentityInfoByNationalCode(
            new GetIdentityInfoByNationalCodeRequestApiM(request.NationalCode, request.BirthDate.ToString()),
            cancellationToken);

        if ( !result.Nin.HasValue() ) {
            throw new CommandValidationException(nameof(request.NationalCode), "کد ملی وارد شده معتبر نمی باشد");
        }

        var person = MapIranian(request, result);
        person.UniqueCode = await personRepo.GetNextYektaCodeAsync();

        if ( request.BankAccountNumber.HasValue() ) {
            person = await InsertSheba(person, cancellationToken);
        }

        if ( request.Mobile.HasValue() ) {
            await ShahkarValidate(person.NationalCode, person.Mobile, cancellationToken);
        }

        await personRepo.InsertAsync(person, cancellationToken: cancellationToken);
        return person.Id;
    }

    private static Person MapIranian(CreateIranianPersonByInquiryCommand personCommand, GetIdentityInfoByNationalCodeResponse identityInfoByNationalCodeResponse) {
        return new Person {
            FirstName = identityInfoByNationalCodeResponse.Name,
            LastName = identityInfoByNationalCodeResponse.Family,
            FatherName = identityInfoByNationalCodeResponse.FatherName,
            BirthDate = identityInfoByNationalCodeResponse.BirthDate.StringDateToInt(),
            NationalCode = personCommand.NationalCode,
            BankAccountNumber = personCommand.BankAccountNumber,
            ShebaNumber = null,
            BirthCertDescription = personCommand.BirthCertDescription,
            BirthCertIssuePlace = identityInfoByNationalCodeResponse.Birthplace,
            BirthCertIssueProvince = identityInfoByNationalCodeResponse.ShenasnameIssuePlace,
            BirthCertNumber = identityInfoByNationalCodeResponse.OfficeCode,
            BirthCertSeri = identityInfoByNationalCodeResponse.CardSeri,
            BirthCertSerial = identityInfoByNationalCodeResponse.ShenasnameSerial?.ToInt(),
            Mobile = personCommand.Mobile,
            DeathCause = null,
            IsDead = identityInfoByNationalCodeResponse.DeathDate.HasValue(),
            Gender = identityInfoByNationalCodeResponse.Gender.ToInt() == 1 ? PersonEntityGender.Male : PersonEntityGender.Female,
            Religion = personCommand.Religion,
            DeathDate = identityInfoByNationalCodeResponse.DeathDate?.StringDateToInt(),
            IsSadat = personCommand.IsSadat,
            Nationality = (short?) Nationality.Iranian,
            MotherName = personCommand.MotherName,
            PersonImage = personCommand.PersonImage.HasValue ? personCommand.PersonImage : null,
            SabteAhvalConfirm = true,
            CreateType = PersonCreateType.WebService
        };
    }

    private async Task<Person> InsertSheba(Person person, CancellationToken cancellationToken) {
        var existingSheba = await personRepo.ExistsAsync(
            p => p.BankAccountNumber == person.BankAccountNumber,
            cancellationToken: cancellationToken);

        if ( existingSheba ) {
            throw new CommandValidationException("شماره شبا وارد شده قبلا ثبت شده است.");
        }

        person.ShebaNumber = await ValidateSheba(person.NationalCode, person.BankAccountNumber, cancellationToken);

        return person;
    }

    private async Task<string> ValidateSheba(string nationalCode, string accountNumber, CancellationToken cancellationToken) {
        var validateSheba = await csisWsmService.ValidateShebaOwnerShip(nationalCode, accountNumber, cancellationToken);

        if ( validateSheba.IsMatched == null || validateSheba.IsMatched == false || validateSheba.ShebaNumber == null ) {
            throw new CommandValidationException(nameof(nationalCode), "شماره شبا متعلق به کد ملی وارد شده نمی‌باشد.");
        }

        return validateSheba.ShebaNumber;
    }

    private async Task ShahkarValidate(string nationalCode, string mobile, CancellationToken cancellationToken) {
        var shahkarResult = await csisWsmService.ValidateMobileOwnership(
            new ValidateMobileOwnershipRequest(nationalCode, mobile),
            cancellationToken);

        if ( !shahkarResult ) {
            throw new CommandValidationException(nameof(mobile), "شماره موبایل متعلق به کد ملی وارد شده نمی‌باشد.");
        }
    }
}
