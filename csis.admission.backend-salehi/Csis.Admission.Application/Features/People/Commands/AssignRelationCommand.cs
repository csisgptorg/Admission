using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using static Csis.Admission.Application.Common.Models.ValidateParentChildRelationshipRequest;
using Csis.Admission.Application.Features.People.Commands;
using MediatR;


namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// انتساب نسبت خانوادگی به شخص
/// </summary>
public sealed record AssignParentChildRelationCommand : BaseCommandDto<AssignParentChildRelationCommand, Person>, IRequest
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
    /// کد ملی والدین
    /// </summary>
    public string ParentNationalCode { get; init; }

    /// <summary>
    /// تاریخ تولد والدین
    /// </summary>
    public string ParentBirthDate { get; init; }

    /// <summary>
    /// کد ملی فرزند
    /// </summary>
    public string ChildNationalCode { get; init; }

    /// <summary>
    /// تاریخ تولد فرزند
    /// </summary>
    public string ChildBirthDate { get; init; }

    /// <summary>
    /// نوع نسبت خانوادگی
    /// </summary>
    public RelationTypeEnum RelationType { get; init; }
}

internal sealed class AssignParentChildRelationCommandHandler(
    IPersonRepository personRepository,
    IPersonMarriageRepository marriageRepository,
    ICsisWsmService csisWsmService,
    ISettingRepository settingRepository)
    : IRequestHandler<AssignParentChildRelationCommand>
{
    public async Task Handle(AssignParentChildRelationCommand request, CancellationToken cancellationToken) {
        var person = await personRepository.GetOneAsTrackingAsync(x => x.Id == request.PersonId, cancellationToken: cancellationToken) ?? throw new CommandValidationException("شخص مورد نظر یافت نشد");

        if ( (person.FatherPersonId.HasValue && request.RelationType == RelationTypeEnum.FatherChild) || (person.MotherPersonId.HasValue && request.RelationType == RelationTypeEnum.MotherChild) ) {
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
            case RelationTypeEnum.FatherChild:
                var fatherResult = await SabteAhvalRelation(request, setting.Value, cancellationToken);
                if ( !fatherResult ) {
                    throw new CommandValidationException("نسبت فرزند در ثبت احوال ثبت نشده است.");
                }
                person.FatherPersonId = request.ParentId;
                break;
            case RelationTypeEnum.MotherChild:
                var motherResult = await SabteAhvalRelation(request, setting.Value, cancellationToken);
                if ( !motherResult ) {
                    throw new CommandValidationException("نسبت فرزند در ثبت احوال ثبت نشده است.");
                }
                person.MotherPersonId = request.ParentId;
                break;
            default:
                throw new CommandValidationException("نوع نسبت خانوادگی نامعتبر است.");
        }

        await AssignChildToSpouse(request.ParentId, person, request.RelationType, cancellationToken);

        await personRepository.UpdateAsync(person, cancellationToken: cancellationToken);
    }


    private async Task<bool> SabteAhvalRelation(AssignParentChildRelationCommand command, RegistrationType registrationType, CancellationToken cancellationToken) {
        // اگر حالت دستی باشد
        if ( registrationType == RegistrationType.Manual ) {
            return true;
        }

        var request = new ValidateParentChildRelationshipRequest(command.ParentNationalCode,
            command.ParentBirthDate,
            command.ChildNationalCode,
            command.ChildBirthDate,
            command.RelationType);

        var result = await csisWsmService.ValidateParentChildRelationship(request, cancellationToken);

        if ( result.GetResult() == ValidateParentChildRelationshipResponse.Result.InvalidNationalCode ) {
            throw new CommandValidationException("کد ملی نامعتبر است.");
        }

        if ( result.GetResult() != ValidateParentChildRelationshipResponse.Result.ValidRelation ) {
            throw new CommandValidationException("نسبت فرزند در ثبت احوال ثبت نشده است.");
        }
        return result.IsRelationFound && result.IsPersonFound;
    }

    /// <summary>
    /// اگر والد ازدواج کرده بود، نسبت فرزند را به همسر والد نیز انتساب می‌دهد
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="person"></param>
    /// <param name="relationType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task AssignChildToSpouse(int parentId, Person person, RelationTypeEnum relationType,
        CancellationToken cancellationToken) {
        var marriages = await marriageRepository.GetOneAsync(x => (x.HusbandPersonId == parentId || x.WifePersonId == parentId), cancellationToken: cancellationToken);
        if ( marriages is null ) {
            return;
        }

        switch ( relationType ) {
            case RelationTypeEnum.FatherChild when marriages.WifePersonId.HasValue:
                person.MotherPersonId = marriages.WifePersonId;
                break;
            case RelationTypeEnum.MotherChild when marriages.HusbandPersonId.HasValue:
                person.FatherPersonId = marriages.HusbandPersonId;
                break;
        }

    }
}
