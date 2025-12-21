using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class CreateAdmissionCaseSecondStepCommandValidator : AbstractValidator<CreateAdmissionCaseSecondStepCommand>
{
    public CreateAdmissionCaseSecondStepCommandValidator() {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("شناسه الزامی است.");
        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("کد تایید موبایل الزامی است.")
            .Length(4).WithMessage("کد تایید موبایل باید حداقل 4 رقم باشد.");
    }
}
