using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

public sealed class CreateAdmissionCaseThirdStepCommandValidator : BaseValidator<CreateAdmissionCaseThirdStepCommand>
{
    public CreateAdmissionCaseThirdStepCommandValidator() {
        RuleFor(x => x.ApprovalCenter).IsInEnum().WithName("مرکز مدیریت حوزوی");
        RuleFor(x => x.CaseNumInApprovalCenter).NotEmpty().WithName("شناسه مرکز مدیریت حوزوی");
    }
}
