using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.CaseFilings.Validator;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// ساخت توکن گام سوم
/// </summary>
public sealed record CreateAdmissionCaseThirdStepCommand : BaseCommandDto<CreateAdmissionCaseThirdStepCommand, AdmissionCaseUser, Guid>, IRequest
{
    /// <summary></summary>
    public Guid Token { get; init; }
    /// <summary></summary>
    public ApprovalCenter ApprovalCenter { get; init; }

    /// <summary></summary>
    public int CaseNumInApprovalCenter { get; init; }
    /// <summary></summary>
    public string NationalCardSerial { get; init; }

    /// <summary> مذهب </summary>
    public Religion Religion { get; init; }
}

internal sealed class CreateAdmissionCaseThirdStepCommandHandler(IRepository<AdmissionCaseUser, Guid> caseUserRepo,ApprovalCenterValidator approvalCenterValidator)
    : IRequestHandler<CreateAdmissionCaseThirdStepCommand>
{
    public async Task Handle(CreateAdmissionCaseThirdStepCommand command, CancellationToken cancellationToken) {
        var admissionCaseUser = await caseUserRepo.GetByIdAsTrackingAsync(command.Token, false, cancellationToken)
                                ?? throw new CommandValidationException("شناسه موقت (توکن) نامعتبر است.");

        if ( admissionCaseUser.Citizenship == Citizenship.Iranian ) {
            await approvalCenterValidator.Iranian(command.CaseNumInApprovalCenter, command.ApprovalCenter, admissionCaseUser.NationalCode,
            admissionCaseUser.BirthDate.IntDateToString(), cancellationToken);
        } else {
            await approvalCenterValidator.NonIranian(command.CaseNumInApprovalCenter, command.ApprovalCenter, admissionCaseUser.YektaCode,
            admissionCaseUser.BirthDate.IntDateToString(), cancellationToken);
        }

        var entity = command.ToEntity(admissionCaseUser);
        entity.CaseStep = AdmissionCaseStep.StudentStatusForRegistrationVerified;

        await caseUserRepo.UpdateAsync(entity, cancellationToken: cancellationToken);
    }
}
