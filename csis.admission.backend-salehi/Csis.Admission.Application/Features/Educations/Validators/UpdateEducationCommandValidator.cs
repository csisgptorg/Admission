using Csis.Admission.Application.Features.Educations.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Educations.Validators;

// validation
internal sealed class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationCommandValidator() {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز باید بزرگتر از صفر باشد.");
        RuleFor(x => x.ApprovalCenter)
                        .IsInEnum().When(x => x.ApprovalCenter.HasValue)
            .WithMessage("مرجع حوزوی نامعتبر است.");
        RuleFor(x => x.CaseNumInApprovalCenter)
            .GreaterThan(0).When(x => x.CaseNumInApprovalCenter.HasValue)
            .WithMessage("شماره پرونده در مرجع حوزوی باید بزرگتر از صفر باشد.");
    }
}
