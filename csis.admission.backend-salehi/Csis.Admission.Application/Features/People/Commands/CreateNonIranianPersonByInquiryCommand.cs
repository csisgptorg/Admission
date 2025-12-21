using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// (ثبت استعلامی فرد غیر ایرانی (با وب سرویس
/// </summary>
public sealed record CreateNonIranianPersonByInquiryCommand : BaseCommandDto<CreateNonIranianPersonByInquiryCommand, Person>, IRequest<int>
{
    /// <summary> شناسه یکتا </summary>
    public string YektaCode { get; init; }

    /// <summary>  تاریخ اعتبار اقامت  </summary>
    public string? ResidenceExpireDate { get; init; }

    /// <summary> شماره حساب </summary>
    public string BankAccountNumber { get; init; }

    /// <summary> شماره شبا </summary>
    public string ShebaNumber { get; init; }

    /// <summary> تلفن همراه </summary>
    public string Mobile { get; init; }

    /// <summary> سیادت </summary>
    public bool IsSadat { get; init; }

    /// <summary> مذهب </summary>
    public Religion Religion { get; init; }

    /// <summary> شناسه تصویر شخص </summary>
    public Guid? PersonImage { get; init; }

    /// <summary> سفارشی سازی نگاشت </summary>
    public override void ReverseCustomMappings(IMappingExpression<CreateNonIranianPersonByInquiryCommand, Person> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.ResidenceExpireDate, opt => opt.MapFrom(src => src.ResidenceExpireDate.StringDateToInt()));
        mapping.ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => (short?) Nationality.NonIranian));
        mapping.ForMember(dest => dest.Citizenship, opt => opt.MapFrom(src => Citizenship.NonIranian));
    }
}

internal sealed class CreateNonIranianPersonByInquiryCommandHandler(ISettingRepository settingRepository, IPersonRepository personRepo, ICsisWsmService csisWsmService) : IRequestHandler<CreateNonIranianPersonByInquiryCommand, int>
{
    public async Task<int> Handle(CreateNonIranianPersonByInquiryCommand request, CancellationToken cancellationToken) {

        await Common.Utilities.ValidateSettings(settingRepository, WebServiceSettingTitle.NonIranian, RegistrationType.Automatic, cancellationToken);


        if ( await personRepo.ExistsAsync(x => x.YektaCode == request.YektaCode && !string.IsNullOrEmpty(x.YektaCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.YektaCode), "شناسه یکتا وارد شده تکراری است");
        }

        var result = await csisWsmService.ValidateNonIranianYektaCode(-1, request.YektaCode, cancellationToken);

        if ( !result.UniqeCode.HasValue ) {
            throw new CommandValidationException(nameof(request.YektaCode), "شناسه یکتا وارد شده معتبر نمی باشد");
        }

        var person = MapNonIranian(request, result);
        person.UniqueCode = await personRepo.GetNextYektaCodeAsync();

        await personRepo.InsertAsync(person, cancellationToken: cancellationToken);
        return person.Id;
    }

    private Person MapNonIranian(CreateNonIranianPersonByInquiryCommand personCommand, ValidateNonIranianYektaCodeResponse nonIranianYektaCodeResponse) {
        return new Person {
            FirstName = nonIranianYektaCodeResponse.FirstName,
            LastName = nonIranianYektaCodeResponse.LastName,
            FatherName = nonIranianYektaCodeResponse.FatherName,
            BirthDate = nonIranianYektaCodeResponse.ShamsiBirthDate.StringDateToInt(),
            YektaCode = nonIranianYektaCodeResponse.UniqeCode.ToString(),
            BankAccountNumber = personCommand.BankAccountNumber,
            ShebaNumber = personCommand.ShebaNumber,
            BirthCertDescription = null,
            Mobile = personCommand.Mobile,
            DeathCause = null,
            Gender = (PersonEntityGender) nonIranianYektaCodeResponse.Gender,
            Religion = personCommand.Religion,
            PassportNumber = nonIranianYektaCodeResponse.PassportNumber,
            MotherName = null,
            ResidenceExpireDate = personCommand.ResidenceExpireDate?.StringDateToInt(),
            Citizenship = Citizenship.NonIranian,
            PersonImage = personCommand.PersonImage,
            Nationality = (short?) Nationality.NonIranian,
            FidaCode = nonIranianYektaCodeResponse.FidaCode?.ToString(),
            IsDead = false,
            IsSadat = personCommand.IsSadat,
            CreateType = PersonCreateType.WebService
        };
    }
}
