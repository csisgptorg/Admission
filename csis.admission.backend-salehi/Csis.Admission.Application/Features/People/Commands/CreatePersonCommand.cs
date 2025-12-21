using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Utilities.Json;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// ایجاد موجودیت شخص جدید
/// </summary>
public sealed record CreatePersonCommand : BaseCommandDto<CreatePersonCommand, Person>, IRequest<int>
{
    /// <summary>  شماره حساب </summary>
    public string BankAccountNumber { get; init; }

    /// <summary> شماره شبا </summary>
    public string ShebaNumber { get; init; }

    /// <summary> توضیحات شناسنامه </summary>
    public string BirthCertDescription { get; init; }

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

    /// <summary> نام پدر </summary>
    public string FatherName { get; init; }

    /// <summary> نام مادر </summary>
    public string MotherName { get; init; }

    /// <summary> شناسه فیدا </summary>
    public string FidaCode { get; init; }

    /// <summary> نام </summary>
    public string FirstName { get; init; }

    /// <summary> نام خانوادگی </summary>
    public string LastName { get; init; }

    /// <summary> تلفن همراه </summary>
    public string Mobile { get; init; }

    /// <summary> کد ملی </summary>
    public string NationalCode { get; init; }

    /// <summary> شماره پاسپورت </summary>
    public string PassportNumber { get; init; }

    /// <summary> ملیت </summary>
    public short Nationality { get; init; }

    /// <summary> تاریخ تولد </summary>
    public string? BirthDate { get; init; }

    /// <summary> مرحوم است </summary>
    public bool IsDead { get; init; }

    /// <summary> سیادت </summary>
    public bool IsSadat { get; init; }

    /// <summary> تاریخ فوت </summary>
    public string? DeathDate { get; init; }

    /// <summary> شناسه یکتا </summary>
    public string YektaCode { get; init; }

    /// <summary>  تاریخ اعتبار اقامت  </summary>
    public string? ResidenceExpireDate { get; init; }

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

    /// <summary> تابعیت </summary>
    public Citizenship Citizenship { get; init; }

    /// <summary> کد یکتا ساخته شده توسط سامانه </summary>
    [JsonIgnore]
    public int UniqueCode { get; init; }

    /// <summary> سفارشی سازی نگاشت </summary>
    /// <param name="mapping"></param>
    public override void ReverseCustomMappings(IMappingExpression<CreatePersonCommand, Person> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.StringDateToInt()));
        mapping.ForMember(dest => dest.ResidenceExpireDate, opt => opt.MapFrom(src => src.ResidenceExpireDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate.StringDateToInt()));
    }
}

internal sealed class CreatePersonCommandHandler(ISettingRepository settingRepository, IPersonRepository personRepo, ILogger<CreatePersonCommandHandler> logger,
        ICsisWsmService csisWsmService) : IRequestHandler<CreatePersonCommand, int>
{
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken) {
        logger.LogDebug("Mapping create person command: {command}", request.ToJson());

        #region GetSettings
        // دریافت تنظیمات ثبت نام بر اساس ملیت شخص
        RegistrationType? setting;
        if ( request.Nationality == (short) Nationality.Iranian ) {
            setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(WebServiceSettingTitle.Iranian.ToString())).Value.ToInt();
        } else if ( request.Nationality == (short) Nationality.NonIranian ) {
            setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(WebServiceSettingTitle.NonIranian.ToString())).Value.ToInt();
        } else {
            throw new CommandValidationException("تنظیمات سیستم یافت نشد");
        }

        #endregion

        #region Validation

        if ( await personRepo.ExistsAsync(x => x.FidaCode == request.FidaCode && !string.IsNullOrEmpty(x.FidaCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.FidaCode), "شناسه فیدا وارد شده تکراری است");
        }

        if ( await personRepo.ExistsAsync(x => x.YektaCode == request.YektaCode && !string.IsNullOrEmpty(x.YektaCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.YektaCode), " شناسه یکتا وارد شده تکراری است");
        }

        if ( await personRepo.ExistsAsync(x => x.NationalCode == request.NationalCode && !string.IsNullOrEmpty(x.NationalCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.NationalCode), "کد ملی وارد شده تکراری است");
        }

        #endregion

        #region WebServiceValidation and Mapping
        Person person = new();

        // اعتبارسنجی اطلاعات وارد شده با وب سرویس ثبت احوال و سامانه اتباع به شرط استفاده از استعلام وب سرویس
        if ( setting is RegistrationType.Automatic ) {

            if ( request.NationalCode.HasValue() ) {
                var result = await csisWsmService.GetIdentityInfoByNationalCode(new GetIdentityInfoByNationalCodeRequestApiM(request.NationalCode,
                request.BirthDate.ToString()), cancellationToken);
                if ( !result.Nin.HasValue() ) {
                    throw new CommandValidationException(nameof(request.NationalCode), "کد ملی وارد شده معتبر نمی باشد");
                }

                person = MapIranian(request, result);
            }

            if ( request.YektaCode.HasValue() ) {
                var result = await csisWsmService.ValidateNonIranianYektaCode(-1, request.YektaCode, cancellationToken);
                if ( !result.UniqeCode.HasValue ) {
                    throw new CommandValidationException(nameof(request.FidaCode), "شناسه یکتا وارد شده معتبر نمی باشد");
                }

                person = MapNonIranian(request, result);
            }
        } else {
            person = request.ToEntity();
            person.CreateType = PersonCreateType.ManualByUser;
        }

        person.UniqueCode = await personRepo.GetNextYektaCodeAsync();
        #endregion

        #region AdditionalValidations
        // ثبت شماره شبا در صورت ایرانی بودن شخص و بررسی نوع تنظیمات
        if ( person.Nationality == (short?) Nationality.Iranian && person.NationalCode.HasValue() ) {
            person = await InsertSheba(person, setting.Value, cancellationToken);
            await ShahkarValidate(person.NationalCode, person.Mobile, cancellationToken);
        }
        #endregion

        #region InsertPerson
        await personRepo.InsertAsync(person, cancellationToken: cancellationToken);
        return person.Id;
        #endregion
    }

    #region MappingMethods
    private Person MapIranian(CreatePersonCommand personCommand, GetIdentityInfoByNationalCodeResponse identityInfoByNationalCodeResponse) {
        return new Person {
            FirstName = identityInfoByNationalCodeResponse.Name,
            LastName = identityInfoByNationalCodeResponse.Family,
            FatherName = identityInfoByNationalCodeResponse.FatherName,
            BirthDate = identityInfoByNationalCodeResponse.BirthDate.StringDateToInt(),
            NationalCode = personCommand.NationalCode,
            BankAccountNumber = personCommand.BankAccountNumber,
            ShebaNumber = personCommand.ShebaNumber,
            BirthCertDescription = personCommand.BirthCertDescription,
            BirthCertIssuePlace = identityInfoByNationalCodeResponse.Birthplace,
            BirthCertIssueProvince = identityInfoByNationalCodeResponse.ShenasnameIssuePlace,
            BirthCertNumber = identityInfoByNationalCodeResponse.OfficeCode,
            BirthCertSeri = identityInfoByNationalCodeResponse.CardSeri,
            BirthCertSerial = identityInfoByNationalCodeResponse.ShenasnameSerial?.ToInt(),
            Mobile = personCommand.Mobile,
            DeathCause = personCommand.DeathCause,
            Citizenship = Citizenship.Iranian,
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

    private Person MapNonIranian(CreatePersonCommand personCommand, ValidateNonIranianYektaCodeResponse nonIranianYektaCodeResponse) {
        return new Person {
            FirstName = nonIranianYektaCodeResponse.FirstName,
            LastName = nonIranianYektaCodeResponse.LastName,
            FatherName = nonIranianYektaCodeResponse.FatherName,
            BirthDate = nonIranianYektaCodeResponse.ShamsiBirthDate.StringDateToInt(),
            YektaCode = nonIranianYektaCodeResponse.UniqeCode.ToString(),
            BankAccountNumber = personCommand.BankAccountNumber,
            ShebaNumber = personCommand.ShebaNumber,
            BirthCertDescription = personCommand.BirthCertDescription,
            Mobile = personCommand.Mobile,
            DeathCause = personCommand.DeathCause,
            Gender = (PersonEntityGender) nonIranianYektaCodeResponse.Gender,
            Religion = personCommand.Religion,
            PassportNumber = nonIranianYektaCodeResponse.PassportNumber,
            MotherName = personCommand.MotherName,
            ResidenceExpireDate = personCommand.ResidenceExpireDate?.StringDateToInt(),
            Citizenship = Citizenship.NonIranian,
            PersonImage = personCommand.PersonImage,
            Nationality = (short?) Nationality.NonIranian,
            FidaCode = nonIranianYektaCodeResponse.FidaCode?.ToString(),
            IsDead = personCommand.IsDead,
            IsSadat = personCommand.IsSadat,
            CreateType = PersonCreateType.WebService
        };
    }
    #endregion

    #region PrivateMethods

    private async Task<Person> InsertSheba(Person person, RegistrationType setting, CancellationToken cancellationToken) {

        var existingSheba = await personRepo.ExistsAsync(
           p => p.BankAccountNumber == person.BankAccountNumber,
           cancellationToken: cancellationToken);

        if ( existingSheba ) {
            throw new CommandValidationException("شماره شبا وارد شده قبلا ثبت شده است.");
        }

        switch ( setting ) {
            case RegistrationType.Manual:
                person.ShebaNumber = person.ShebaNumber;
                person.BankAccountNumber = person.BankAccountNumber;
                break;
            case RegistrationType.Automatic:
            default:
                person.ShebaNumber = await ValidateSheba(person.NationalCode, person.BankAccountNumber, cancellationToken);
                person.BankAccountNumber = person.BankAccountNumber;
                break;
        }
        ;
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
        var shahkarResult = await csisWsmService.ValidateMobileOwnership(new ValidateMobileOwnershipRequest(nationalCode, mobile), cancellationToken);

        if(!shahkarResult ) {
            throw new CommandValidationException(nameof(mobile), "شماره موبایل متعلق به کد ملی وارد شده نمی‌باشد.");
        }
    }

    #endregion
}
