using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.NonStudentDependants.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonStudentDependants.Validators;

public sealed class CreateNonStudentDependantCommandValidator : BaseValidator<CreateNonStudentDependantCommand>
{
    public CreateNonStudentDependantCommandValidator() {
        RuleFor(x => x.Relationship).IsInEnum().WithName("نسبت");
    }
}
