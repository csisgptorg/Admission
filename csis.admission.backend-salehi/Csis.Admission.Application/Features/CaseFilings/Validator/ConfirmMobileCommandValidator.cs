using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class ConfirmMobileCommandValidator : AbstractValidator<CreateAdmissionCaseSecondStepCommand>
{
    public ConfirmMobileCommandValidator() {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("احراز هویت الزامی است , لطفا دوباره تلاش کنید");
        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("کد تایید موبایل الزامی است.")
            .MaximumLength(6).WithMessage("کد تایید موبایل نباید بیشتر از 6 کاراکتر باشد.");
    }
}
