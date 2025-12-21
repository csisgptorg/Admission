using Csis.Admission.Application.Features.BlockedServices.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseBlock.Validators;

// validation
internal sealed class CreateStudentCaseBlockRequestCommandValidator : AbstractValidator<CreateStudentCaseBlockRequestCommand>
{
    public CreateStudentCaseBlockRequestCommandValidator() {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز باید بزرگتر از صفر باشد.");
        RuleFor(x => x.CaseBlockReasonId)
            .NotEmpty().WithMessage("حداقل یک دلیل مسدودی باید انتخاب شود.");
    }
}
