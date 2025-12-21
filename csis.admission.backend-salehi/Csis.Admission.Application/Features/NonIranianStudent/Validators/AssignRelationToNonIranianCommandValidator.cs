using Csis.Admission.Application.Features.NonIranianStudent.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonIranianStudent.Validators;

public sealed class AssignRelationToNonIranianCommandValidator : BaseValidator<AssignRelationToNonIranianCommand>
{
    public AssignRelationToNonIranianCommandValidator() {
        RuleFor(x => x.StudentYektaCode).NotNull().WithMessage("کد یکتا طلبه نمی تواند خالی باشد");
        RuleFor(x => x.DependentYektaCode).NotNull().WithMessage("کد یکتا طلبه نمی تواند خالی باشد");
    }
}
