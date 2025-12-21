using Csis.Admission.Application.Features.StudentMobiles.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.StudentMobiles.Validators;

// validation
internal sealed class UpdateDependentMobileRequestCommandValidator : AbstractValidator<UpdateDependentMobileRequestCommand>
{
    public UpdateDependentMobileRequestCommandValidator() {
        RuleFor(r => r.Codm)
            .GreaterThan(0).WithMessage("کد مرکز معتبر نمی باشد.");
        RuleFor(r => r.DependentId)
            .GreaterThan(0).WithMessage("شناسه تکفل معتبر نمی باشد.");
        RuleFor(r => r.Mobile)
            .NotEmpty().WithMessage("شماره موبایل نمی تواند خالی باشد.");
    }
}
