using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <inheritdoc/>
public sealed record UpdateDependentDivorceRequestCommand : IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }

    /// <inheritdoc/>
    public string DependentNationalCode { get; init; }

    /// <inheritdoc/>
    public string DependentBirthDate { get; init; }

    /// <summary> </summary>
    public string DependentSpouseNationalCode { get; init; }
    /// <inheritdoc/>
    public string DependentSpouseBirthDate { get; init; }
}

internal sealed class UpdateDependentDivorceCommandHandler(
    IStudentRepository studentRepository,
    ICurrentUserService currentUserService,
    ICsisWsmService csisWsmService,
    IRepository<DependentSummary, long> studentDependentRepo,
    IRequestService requestService)
    : IRequestHandler<UpdateDependentDivorceRequestCommand>
{

    public async Task Handle(UpdateDependentDivorceRequestCommand request, CancellationToken cancellationToken) {

        var student = await studentRepository.GetStudentInfoByCodm(request.Codm)
                      ?? throw new CommandValidationException(" کد مرکز صحیح نیست ");

        var dependent = await studentDependentRepo.GetByIdAsync(request.DependentId.Value, cancellationToken: cancellationToken)
                        ?? throw new CommandValidationException(" آیدی تکفل صحیح نیست ");

        if ( !student.IsMarried ) {
            throw new CommandValidationException(" طلبه مجرد می باشد ");
        }
        //TODO: سید sp رو بررسی کنه, با اینکه مزدوجه ولی خروجی اشتباه برمیگردونه و میگه متاهل نیست
        //if ( !dependent.IsMarried ) {
        //    throw new CommandValidationException("تکفل انتخاب شده متاهل نمی باشد ");
        //}

        if ( dependent.IsDead ) {
            throw new CommandValidationException(" تکفل انتخاب شده مرحوم می باشد ");
        }
        if ( student.Citizenship != Citizenship.Iranian ) {
            throw new CommandValidationException(" این عملیات فقط برای طلاب ایرانی امکان پذیر است ");
        }

        var spousal = new ValidateSpousalRelationshipRequest(request.Codm, request.DependentNationalCode, request.DependentBirthDate.StringDateToInt().Value, request.DependentSpouseNationalCode, request.DependentSpouseBirthDate, request.DivorceDate, ValidateSpousalRelationshipRequest.RelationTypeEnum.Divorce);
        var response = await csisWsmService.ValidateSpousalRelationship(spousal, cancellationToken);
        if ( response is ValidateSpousalRelationshipResponse.Result.InvalidNationalCode ) {
            throw new CommandValidationException("شماره ملی همسر نامعتبر می باشد.");
        }

        if ( response is not ValidateSpousalRelationshipResponse.Result.ValidRelation ) {
            throw new CommandValidationException("اطلاعات طلاق در ثبت احوال ثبت نشده است.");
        }

        var flow = (await currentUserService.PersonnelId()).HasValue ? RequestFlow.DirectRegistration : RequestFlow.StudentToEmployeeToSeniorEmployee;
        var requestCommand = new CreateRequestCommand(request, flow);
        await requestService.Create(requestCommand, cancellationToken);
    }

}
