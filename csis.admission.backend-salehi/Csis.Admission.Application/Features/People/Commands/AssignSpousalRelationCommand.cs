using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.People.Commands;


/// <summary>
/// انتساب همسر
/// </summary>
public sealed record AssignSpousalRelationCommand : BaseCommandDto<AssignSpousalRelationCommand, Marriage>, IRequest
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
    /// نوع واقعه
    /// </summary>
    public ValidateSpousalRelationshipRequest.RelationTypeEnum RelationType { get; init; }

    public override void ReverseCustomMappings(IMappingExpression<AssignSpousalRelationCommand, Marriage> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.MarriageDate, opt => opt.MapFrom(src => src.MarriageDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DivorceDate, opt => opt.MapFrom(src => src.DivorceDate.StringDateToInt()));
        mapping.ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate.StringDateToInt()));
    }
}

internal sealed class AssignSpousalRelationCommandHandler(
    IPersonRepository personRepository,
    IRepository<Marriage> marriageRepository,
    ICsisWsmService csisWsmService,
    ISettingRepository settingRepository)
    : IRequestHandler<AssignSpousalRelationCommand>
{
    public async Task Handle(AssignSpousalRelationCommand request, CancellationToken cancellationToken) {
        if ( await marriageRepository.ExistsAsync(
                x => x.HusbandPersonId == request.HusbandPersonId && x.WifePersonId == request.WifePersonId && x.MarriageDate.HasValue,
                cancellationToken: cancellationToken) && request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Marriage ) {
            throw new CommandValidationException("ازدواج بین این دو شخص قبلا ثبت شده است");
        }

        if ( await marriageRepository.ExistsAsync(
                x => x.HusbandPersonId == request.HusbandPersonId && x.WifePersonId == request.WifePersonId && x.DivorceDate.HasValue,
                cancellationToken: cancellationToken) && request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Divorce ) {
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

        //TODO: تبدیل ها به ctor منتقل شود
        var validateSpousalRelationshipRequest = new ValidateSpousalRelationshipRequest(0,
            husband.NationalCode, husband.BirthDate.Value
             , wife.NationalCode, wife.BirthDate.Value.ToString(),
             request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Marriage
                 ? request.MarriageDate
                 : request.DivorceDate, request.RelationType);

        var result = await SabteAhvalRelation(validateSpousalRelationshipRequest, setting.Value, cancellationToken);

        var entity = request.ToEntity();
        await marriageRepository.InsertAsync(entity, cancellationToken: cancellationToken);
        if ( result ) {
            await AssignChildToSpouse(request.HusbandPersonId.Value, request.WifePersonId.Value, cancellationToken);
        }
    }


    private async Task<bool> SabteAhvalRelation(ValidateSpousalRelationshipRequest request, RegistrationType registrationType, CancellationToken cancellationToken) {
        // اگر حالت دستی باشد، اعتبارسنجی را رد کنیم
        if ( registrationType == RegistrationType.Manual ) {
            return true;
        }

        var result = await csisWsmService.ValidateSpousalRelationship(request, cancellationToken);

        return result switch {
            ValidateSpousalRelationshipResponse.Result.InvalidNationalCode => throw new CommandValidationException(
                $@"خطای استعلام {(request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Marriage ? "ازدواج" : "طلاق")}"),
            ValidateSpousalRelationshipResponse.Result.ValidNationalCode => throw new CommandValidationException(
                "کد ملی افراد معتبر است اما نسبت خانوادگی در ثبت احوال ثبت نشده است"),
            _ => result == ValidateSpousalRelationshipResponse.Result.ValidRelation
        };
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
