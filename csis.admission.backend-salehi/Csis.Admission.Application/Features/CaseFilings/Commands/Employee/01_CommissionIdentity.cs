using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Features.CaseFilings.Validator;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>ثبت اطلاعات هویتی از کمیسیون</summary>
public sealed record CreateAdmissionCaseCommissionIdentityByEmployeeCommand : IRequest<AdmissionCaseStepDto>
{
    /// <summary>شناسه درخواست</summary>
    public int CommissionRequestId { get; init; }

    /// <summary>تایید</summary>
    public bool? Confirmed { get; set; }
}

internal sealed class CreateAdmissionCaseCommissionIdentityByEmployeeCommandHandler(IStudentRepository studentRepo,
    IRepository<AdmissionCaseUser, Guid> admissionCaseUserRepo, IdentityValidator identityValidator, ApprovalCenterValidator approvalCenterValidator)
    : IRequestHandler<CreateAdmissionCaseCommissionIdentityByEmployeeCommand, AdmissionCaseStepDto>
{
    public async Task<AdmissionCaseStepDto> Handle(CreateAdmissionCaseCommissionIdentityByEmployeeCommand command, CancellationToken cancellationToken) {

        // validate status
        var commission = await ValidateStatus(command, cancellationToken);
        var admissionCase = await GetAdmissionCase(commission, cancellationToken);
        if ( admissionCase != null ) { return new AdmissionCaseStepDto(admissionCase.Id, admissionCase.CaseStep.Value); }

        if ( command.Confirmed != true ) { throw new ConfirmedValidationException(commission); }

        // validate iranian
        if ( commission.Citizenship == Citizenship.Iranian ) {
            await approvalCenterValidator.Iranian(command.CommissionRequestId, ApprovalCenter.Commission, commission.NationalCode, commission.BirthDate.IntDateToString(), cancellationToken);
            await identityValidator.Iranian(commission.NationalCode, commission.BirthDate.IntDateToString(), commission.Mobile, cancellationToken);

            // validate non iranian
        } else {
            await approvalCenterValidator.NonIranian(command.CommissionRequestId, ApprovalCenter.Commission, commission.YektaCode, commission.BirthDate.IntDateToString(), cancellationToken);
            await identityValidator.NonIranian(commission.YektaCode, commission.BirthDate.IntDateToString(), cancellationToken);
        }

        // insert
        admissionCase = await Insert(commission, cancellationToken);
        return new AdmissionCaseStepDto(admissionCase.Id, admissionCase.CaseStep.Value);
    }

    private async Task<CommissionStudentIdentityDto> ValidateStatus(CreateAdmissionCaseCommissionIdentityByEmployeeCommand command, CancellationToken cancellationToken) {
        var commission = await studentRepo.GetCommissionForNewStudent(command.CommissionRequestId);

        if ( commission.Status != Enums.CommissionRequestStatus.CommissionApproved &&
            commission.Status != Enums.CommissionRequestStatus.BranchExpertActionPending ) {
            throw new CommandValidationException("امکان تشکیل پرونده به دلیل وضعیت فعلی کمیسیون وجود ندارد. وضعیت کمیسیون: " + commission.StatusTitle);
        }

        return commission;
    }

    private async Task<AdmissionCaseUser> GetAdmissionCase(CommissionStudentIdentityDto commission, CancellationToken cancellationToken) {
        return await admissionCaseUserRepo.GetOneAsTrackingAsync(x =>
            (commission.NationalCode != null && x.NationalCode == commission.NationalCode) ||
            (commission.YektaCode != null && x.YektaCode == commission.YektaCode), false, cancellationToken);
    }

    private async Task<AdmissionCaseUser> Insert(CommissionStudentIdentityDto commission, CancellationToken cancellationToken) {
        var admissionCase = commission.ToEntity();
        var identityInfoPayload = new ValidateIdentityDto(commission.FirstName, commission.LastName, commission.FatherName, commission.Citizenship,
                commission.BirthDate.IntDateToString(), commission.NationalCode, commission.YektaCode, commission.Gender, commission.StatusTitle,
                commission.CommissionRequestId);
        admissionCase.Payloads = PayloadHelper.AddPayloadsToString(identityInfoPayload, admissionCase.Payloads, nameof(AdmissionCasePayloadName.Identity));
        admissionCase.CaseStep = AdmissionCaseStep.IdentityVerified;
        await admissionCaseUserRepo.InsertAsync(admissionCase, true, cancellationToken);

        return admissionCase;
    }
}
