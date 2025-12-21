using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// ویرایش موجودیت شخص
/// </summary>
public sealed record UpdatePersonCommand : BaseCommandDto<UpdatePersonCommand, Person>, IRequest
{
    /// <summary>
    /// شناسه موجودیت شخص
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// سیادت
    /// </summary>
    public bool IsSadat { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; init; }

    /// <summary>
    /// نام مادر
    /// </summary>
    public string MotherName { get; init; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public int? BirthDate { get; init; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public PersonEntityGender Gender { get; init; }

    /// <summary>
    /// مذهب
    /// </summary>
    public Religion Religion { get; init; }

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship Citizenship { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
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
    //Nationality table or use tb16(shared api)

    /// <summary>
    /// شماره پاسپورت
    /// </summary>
    public string PassportNumber { get; init; }

    /// <summary>
    /// شناسه فیدا
    /// </summary>
    public string FidaCode { get; init; }

    /// <summary>
    /// شناسه یکتا
    /// </summary>
    public string YektaCode { get; init; }

    /// <summary>
    /// تاریخ اعتبار اقامت
    /// </summary>
    public int? ResidenceExpireDate { get; init; }

    /// <summary>
    /// مرحوم است
    /// </summary>
    public bool IsDead { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public int? DeathDate { get; init; }

    /// <summary>
    /// علت فوت
    /// </summary>
    public DeathCause? DeathCause { get; init; }

    /// <summary>
    /// مورد تایید ثبت احوال می باشد
    /// </summary>
    public bool SabteAhvalConfirm { get; init; }

    /// <summary>
    /// تلفن همراه
    /// </summary>
    public string Mobile { get; init; }

    /// <summary>
    /// شماره حساب
    /// </summary>
    public string BankAccountNumber { get; init; }
    //multi?

    /// <summary>
    /// شماره شبا
    /// </summary>
    public string ShebaNumber { get; init; }

    /// <summary>
    /// شناسه مادر
    /// </summary>
    public int? MotherPersonId { get; init; }

    /// <summary>
    /// شناسه پدر
    /// </summary>
    public int? FatherPersonId { get; init; }

    /// <summary>
    /// شناسه تصویر شخص
    /// </summary>
    public Guid? PersonImage { get; init; }

    /// <summary>
    /// کد یکتا ساخته شده توسط سامانه
    /// </summary>
}
internal sealed class UpdatePersonCommandHandler(
IPersonRepository personRepo,
IMapper mapper,
ILogger<UpdatePersonCommandHandler> logger)
: IRequestHandler<UpdatePersonCommand>
{

    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken) {
        logger.LogDebug("Updating person with id {id}", request.Id);

        var person = await personRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("شخص یافت نشد.");

        if ( person.CreateType != PersonCreateType.ManualByUser ) {
            throw new CommandValidationException("امکان ویرایش این شخص وجود ندارد.");
        }

        person = mapper.Map(request, person);

        logger.LogDebug("Person with id {id} after update: {after}", request.Id, person.ToJson());

        await personRepo.UpdateAsync(person, cancellationToken: cancellationToken);
    }
}

