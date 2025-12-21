using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.People.Commands;


/// <summary>
/// انتساب همسر برای افراد غیر ایرانی
/// </summary>
public sealed record AssignNonIranianSpousalRelationCommand : BaseCommandDto<AssignNonIranianSpousalRelationCommand, Marriage>, IRequest
{
    /// <summary>
    /// شناسه شوهر
    /// </summary>
    public int? HusbandPersonId { get; init; }

    /// <summary>
    /// شناسه همسر
    /// </summary>
    public int? WifePersonId { get; init; }

    /// <summary>
    /// کد یکتای شوهر
    /// </summary>
    public string HusbandYektaCode { get; init; }

    /// <summary>
    /// کد یکتای همسر
    /// </summary>
    public string WifeYektaCode { get; init; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public string? MarriageDate { get; init; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public string? DivorceDate { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public string? DeathDate { get; init; }

    /// <summary>
    /// نوع رابطه - همسر
    /// </summary>
    public ValidateNonIranianRelationshipResponse.NonIranianDependentRelation RelationType { get; init; } = ValidateNonIranianRelationshipResponse.NonIranianDependentRelation.Spouse;

    public override void ReverseCustomMappings(IMappingExpression<AssignNonIranianSpousalRelationCommand, Marriage> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.MarriageDate, opt => opt.MapFrom(src => src.MarriageDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DivorceDate, opt => opt.MapFrom(src => src.DivorceDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate.StringDateToInt()));
    }
}

internal sealed class AssignNonIranianSpousalRelationCommandHandler(
    IPersonRepository personRepository,
    IRepository<Marriage> marriageRepository,
    ICsisWsmService csisWsmService,
    ISettingRepository settingRepository)
    : IRequestHandler<AssignNonIranianSpousalRelationCommand>
{
    public async Task Handle(AssignNonIranianSpousalRelationCommand request, CancellationToken cancellationToken) {
        if ( await marriageRepository.ExistsAsync(
                x => x.HusbandPersonId == request.HusbandPersonId && x.WifePersonId == request.WifePersonId && x.MarriageDate.HasValue,
                cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("ازدواج بین این دو شخص قبلا ثبت شده است");
        }

        if ( await marriageRepository.ExistsAsync(
                x => x.HusbandPersonId == request.HusbandPersonId && x.WifePersonId == request.WifePersonId && x.DivorceDate.HasValue,
                cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("طلاق بین این دو شخص قبلا ثبت شده است");
        }

        var husband = await personRepository.GetOneAsync(x => x.Id == request.HusbandPersonId, cancellationToken: cancellationToken)
                 ?? throw new CommandValidationException("شخص مورد نظر یافت نشد");

        var wife = await personRepository.GetOneAsync(x => x.Id == request.WifePersonId, cancellationToken: cancellationToken)
                     ?? throw new CommandValidationException("شخص مورد نظر یافت نشد");

        // دریافت تنظیمات ثبت نام بر اساس ملیت شوهر (یا همسر)
        RegistrationType? setting;
        if ( husband.Nationality == (short) Nationality.Iranian ) {
            setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(WebServiceSettingTitle.Iranian.ToString())).Value.ToInt();
        } else if ( husband.Nationality == (short) Nationality.NonIranian ) {
            setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(WebServiceSettingTitle.NonIranian.ToString())).Value.ToInt();
        } else {
            throw new CommandValidationException("تنظیمات سیستم یافت نشد");
        }

        var result = await ValidateNonIranianRelation(request, setting.Value, cancellationToken);

        var entity = request.ToEntity();
        await marriageRepository.InsertAsync(entity, cancellationToken: cancellationToken);
        if ( result ) {
            await AssignChildToSpouse(request.HusbandPersonId.Value, request.WifePersonId.Value, cancellationToken);
        }
    }


    private async Task<bool> ValidateNonIranianRelation(AssignNonIranianSpousalRelationCommand command, RegistrationType registrationType, CancellationToken cancellationToken) {
        // اگر حالت دستی باشد، اعتبارسنجی را رد کنیم
        if ( registrationType == RegistrationType.Manual ) {
            return true;
        }

        var validateRequest = new ValidateNonIranianRelationshipRequest(command.HusbandYektaCode, command.WifeYektaCode);

        var response = await csisWsmService.ValidateNonIranianRelationship(validateRequest, cancellationToken);

        var relationshipResult = response.GetResult();

        if ( relationshipResult == ValidateNonIranianRelationshipResponse.Result.InvalidYektaCode ) {
            throw new CommandValidationException("کد یکتای افراد معتبر نیست");
        }

        // بررسی نوع رابطه
        if ( Enum.TryParse<ValidateNonIranianRelationshipResponse.NonIranianDependentRelation>(response.RelationId, out var parsedRelation) ) {
            if ( parsedRelation != ValidateNonIranianRelationshipResponse.NonIranianDependentRelation.Spouse ) {
                throw new CommandValidationException("رابطه بین این دو شخص از نوع همسر نیست");
            }

            return relationshipResult == ValidateNonIranianRelationshipResponse.Result.ValidRelation;
        }

        throw new CommandValidationException("نسبت خانوادگی در المصطفی ثبت نشده است");
    }

    //اگر پدر و مادر هر کدام فرزند دارند برای پدر و مادر تمامی فرزندان ثبت شود
    private async Task AssignChildToSpouse(int fatherId, int motherId, CancellationToken cancellationToken) {
        var children = await personRepository.GetAllAsTrackingAsync(x => x.FatherPersonId == fatherId || x.MotherPersonId == motherId, cancellationToken: cancellationToken);

        foreach ( var child in children ) {
            if ( !child.FatherPersonId.HasValue ) {
                child.FatherPersonId = fatherId;
            }

            if ( !child.MotherPersonId.HasValue ) {
                child.MotherPersonId = motherId;
            }
        }
        await personRepository.UpdateAsync(children, cancellationToken: cancellationToken);
    }

}
