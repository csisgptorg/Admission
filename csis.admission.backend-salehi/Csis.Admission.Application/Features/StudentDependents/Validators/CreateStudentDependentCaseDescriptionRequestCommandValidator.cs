using Csis.Admission.Application.Features.StudentDependents.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.StudentDependents.Validators;

// validation
internal sealed class CreateStudentDependentCaseDescriptionRequestCommandValidator : AbstractValidator<CreateStudentDependentCaseDescriptionRequestCommand>
{
    public CreateStudentDependentCaseDescriptionRequestCommandValidator() {
        RuleFor(r => r.Codm)
            .GreaterThan(0).WithMessage("کد مرکز معتبر نمی باشد.");
        RuleFor(r => r.DependentId)
            .GreaterThan(0).WithMessage("شناسه تکفل معتبر نمی باشد.");
        RuleFor(r => r.CaseDescription)
            .NotEmpty().WithMessage("توضیحات پرونده تکفل نمی تواند خالی باشد.")
            .MaximumLength(2000).WithMessage("توضیحات پرونده تکفل نمی تواند بیشتر از 2000 کاراکتر باشد.");
    }
}
