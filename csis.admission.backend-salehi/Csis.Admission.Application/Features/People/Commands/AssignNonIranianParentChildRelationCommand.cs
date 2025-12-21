using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// انتساب نسبت خانوادگی به شخص غیر ایران?
/// </summary>
public sealed record AssignNonIranianParentChildRelationCommand : BaseCommandDto<AssignNonIranianParentChildRelationCommand, Person>, IRequest
{
    /// <summary>
    /// شناسه شخص
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// شناسه والد
    /// </summary>
    public int ParentId { get; init; }

    /// <summary>
    /// کد ?کتا? والد?ن
    /// </summary>
    public string ParentYektaCode { get; init; }

    /// <summary>
    /// کد ?کتا? فرزند
    /// </summary>
    public string ChildYektaCode { get; init; }

    /// <summary>
    /// نوع نسبت خانوادگ?
    /// </summary>
    public RelationTypeEnum RelationType { get; init; }

    /// <summary>
    /// نوع نسبت خانوادگ?
    /// </summary>
    public enum RelationTypeEnum
    {
        /// <summary>
        /// رابطه پدر و فرزند
        /// </summary>
        FatherChild = 1,

        /// <summary>
        /// رابطه مادر و فرزند
        /// </summary>
        MotherChild = 2
    }
}

internal sealed class AssignNonIranianParentChildRelationCommandHandler(
    IPersonRepository personRepository,
    IPersonMarriageRepository marriageRepository,
    ICsisWsmService csisWsmService,
    ISettingRepository settingRepository)
    : IRequestHandler<AssignNonIranianParentChildRelationCommand>
{
    public async Task Handle(AssignNonIranianParentChildRelationCommand request, CancellationToken cancellationToken) {
        var person = await personRepository.GetOneAsTrackingAsync(x => x.Id == request.PersonId, cancellationToken: cancellationToken) 
            ?? throw new CommandValidationException("شخص مورد نظر یافت نشد");

        if ( (person.FatherPersonId.HasValue && request.RelationType == AssignNonIranianParentChildRelationCommand.RelationTypeEnum.FatherChild) 
            || (person.MotherPersonId.HasValue && request.RelationType == AssignNonIranianParentChildRelationCommand.RelationTypeEnum.MotherChild) ) {
            throw new CommandValidationException(" نسبت خانوادگی برای این فرد ثبت شده است");
        }

        // دریافت تنظیمات ثبت نام بر اساس ملیت شخص
        RegistrationType? setting;
        if ( person.Nationality == (short) Nationality.Iranian ) {
            setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(WebServiceSettingTitle.Iranian.ToString())).Value.ToInt();
        } else if ( person.Nationality == (short) Nationality.NonIranian ) {
            setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(WebServiceSettingTitle.NonIranian.ToString())).Value.ToInt();
        } else {
            throw new CommandValidationException("تنظیمات سیستم یافت نشد");
        }

        switch ( request.RelationType ) {
            case AssignNonIranianParentChildRelationCommand.RelationTypeEnum.FatherChild:
                var fatherResult = await ValidateNonIranianRelation(request, setting.Value, cancellationToken);
                if ( !fatherResult ) {
                    throw new CommandValidationException("نسبت فرزند در المصطفی ثبت نشده است.");
                }
                person.FatherPersonId = request.ParentId;
                break;
            case AssignNonIranianParentChildRelationCommand.RelationTypeEnum.MotherChild:
                var motherResult = await ValidateNonIranianRelation(request, setting.Value, cancellationToken);
                if ( !motherResult ) {
                    throw new CommandValidationException("نسبت فرزند در المصطفی ثبت نشده است.");
                }
                person.MotherPersonId = request.ParentId;
                break;
            default:
                throw new CommandValidationException("نوع نسبت خانوادگی نامعتبر است.");
        }

        await AssignChildToSpouse(request.ParentId, person, request.RelationType, cancellationToken);

        await personRepository.UpdateAsync(person, cancellationToken: cancellationToken);
    }


    private async Task<bool> ValidateNonIranianRelation(AssignNonIranianParentChildRelationCommand command, RegistrationType registrationType, CancellationToken cancellationToken) {
        // اگر حالت دستی باشد
        if ( registrationType == RegistrationType.Manual ) {
            return true;
        }

        var validateRequest = new ValidateNonIranianRelationshipRequest(command.ParentYektaCode, command.ChildYektaCode);

        var response = await csisWsmService.ValidateNonIranianRelationship(validateRequest, cancellationToken);

        var relationshipResult = response.GetResult();

        if ( relationshipResult == ValidateNonIranianRelationshipResponse.Result.InvalidYektaCode ) {
            throw new CommandValidationException("کد یکتای افراد معتبر نیست");
        }

        // بررس? نوع رابطه
        if ( Enum.TryParse<ValidateNonIranianRelationshipResponse.NonIranianDependentRelation>(response.RelationId, out var parsedRelation) ) {
            if ( parsedRelation != ValidateNonIranianRelationshipResponse.NonIranianDependentRelation.Child ) {
                throw new CommandValidationException("رابطه بین این دو شخص از نوع فرزند نیست");
            }

            return relationshipResult == ValidateNonIranianRelationshipResponse.Result.ValidRelation && response.IsRelationFound;
        }

        throw new CommandValidationException("نسبت خانوادگی در المصطفی ثبت نشده است");
    }

    /// <summary>
    /// اگر والد ازدواج کرده بود، نسبت فرزند را به همسر والد نیز انتساب می‌دهد
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="person"></param>
    /// <param name="relationType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task AssignChildToSpouse(int parentId, Person person, AssignNonIranianParentChildRelationCommand.RelationTypeEnum relationType,
        CancellationToken cancellationToken) {
        var marriages = await marriageRepository.GetOneAsync(x => (x.HusbandPersonId == parentId || x.WifePersonId == parentId), cancellationToken: cancellationToken);
        if ( marriages is null ) {
            return;
        }

        switch ( relationType ) {
            case AssignNonIranianParentChildRelationCommand.RelationTypeEnum.FatherChild when marriages.WifePersonId.HasValue:
                person.MotherPersonId = marriages.WifePersonId;
                break;
            case AssignNonIranianParentChildRelationCommand.RelationTypeEnum.MotherChild when marriages.HusbandPersonId.HasValue:
                person.FatherPersonId = marriages.HusbandPersonId;
                break;
        }

    }
}
