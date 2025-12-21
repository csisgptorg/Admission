using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// ثبت دستی فرد غیر ایرانی
/// </summary>
public sealed record CreateNonIranianPersonManuallyCommand : BaseCommandDto<CreateNonIranianPersonManuallyCommand, Person>, IRequest<int>
{
    /// <summary> نام </summary>
    public string FirstName { get; init; }

    /// <summary> نام خانوادگی </summary>
    public string LastName { get; init; }

    /// <summary> شماره پاسپورت </summary>
    public string PassportNumber { get; init; }

    /// <summary> تاریخ تولد </summary>
    public string? BirthDate { get; init; }

    /// <summary> شناسه یکتا </summary>
    public string YektaCode { get; init; }

    /// <summary>  تاریخ اعتبار اقامت  </summary>
    public string? ResidenceExpireDate { get; init; }

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
    public override void ReverseCustomMappings(IMappingExpression<CreateNonIranianPersonManuallyCommand, Person> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.StringDateToInt()));
        mapping.ForMember(dest => dest.ResidenceExpireDate, opt => opt.MapFrom(src => src.ResidenceExpireDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate.StringDateToInt()));
        mapping.ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => (short?) Nationality.NonIranian));
        mapping.ForMember(dest => dest.Citizenship, opt => opt.MapFrom(src => Citizenship.NonIranian));
    }
}
internal sealed class CreateNonIranianPersonManuallyCommandHandler(ISettingRepository settingRepository, IPersonRepository personRepo) : IRequestHandler<CreateNonIranianPersonManuallyCommand, int>
{
    public async Task<int> Handle(CreateNonIranianPersonManuallyCommand request, CancellationToken cancellationToken) {

        await Common.Utilities.ValidateSettings(settingRepository, WebServiceSettingTitle.NonIranian, RegistrationType.Manual, cancellationToken);

        if ( await personRepo.ExistsAsync(x => x.YektaCode == request.YektaCode && !string.IsNullOrEmpty(x.YektaCode), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.YektaCode), "شناسه یکتا وارد شده تکراری است");
        }

        var person = request.ToEntity();
        person.CreateType = PersonCreateType.ManualByUser;
        person.UniqueCode = await personRepo.GetNextYektaCodeAsync();

        await personRepo.InsertAsync(person, cancellationToken: cancellationToken);
        return person.Id;
    }
}
