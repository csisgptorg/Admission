using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

// validation
internal sealed class ValidateShebaOwnershipCommandValidator : AbstractValidator<ValidateShebaOwnershipCommand>
{
    public ValidateShebaOwnershipCommandValidator() {
        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی نمی‌تواند خالی باشد.")
            .Length(10).WithMessage("کد ملی باید 10 رقم باشد.");

        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("شماره حساب نمی‌تواند خالی باشد.")
            .Length(10).WithMessage("شماره حساب باید 10 رقم باشد.");
    }
}
