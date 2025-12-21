using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.DependentCaseActive.Validators;

// validation
internal sealed class UpdateDependentCaseActiveStatusSeniorRequestCommandValidator : AbstractValidator<UpdateDependentCaseActiveStatusSeniorRequestCommand>
{
    public UpdateDependentCaseActiveStatusSeniorRequestCommandValidator() {
        RuleFor(r => r.Codm)
            .GreaterThan(0).WithMessage("کد مرکز معتبر نمی باشد.");
        RuleFor(r => r.DependentId)
            .GreaterThan(0).WithMessage("شناسه تکفل معتبر نمی باشد.");
        RuleFor(r => r.ActiveReason)
            .IsInEnum().WithMessage("دلیل فعال بودن تکفل معتبر نمی باشد.");
    }
}
