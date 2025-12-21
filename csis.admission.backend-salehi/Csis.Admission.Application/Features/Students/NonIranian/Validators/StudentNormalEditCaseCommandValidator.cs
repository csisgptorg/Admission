using Csis.Admission.Application.Features.Students.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Students.NonIranian.Validators;

// validation
internal sealed class StudentNormalEditCaseCommandValidator : AbstractValidator<StudentNormalEditCaseCommand>
{
    public StudentNormalEditCaseCommandValidator() {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز طلبه باید بزرگتر از صفر باشد.");

        RuleFor(x => x.CaseDescription)
            .NotEmpty().WithMessage("توضیحات پرونده نمی تواند خالی باشد.")
            .MaximumLength(2000).WithMessage("توضیحات پرونده نمی تواند بیشتر از 2000 کاراکتر باشد.");
    }
}
