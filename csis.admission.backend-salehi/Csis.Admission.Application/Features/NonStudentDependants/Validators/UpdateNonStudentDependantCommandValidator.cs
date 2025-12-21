using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.NonStudentDependants.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonStudentDependants.Validators;

public sealed class UpdateNonStudentDependantCommandValidator : BaseValidator<UpdateNonStudentDependantCommand>
{
    public UpdateNonStudentDependantCommandValidator() {
        RuleFor(x => x.Relationship).IsInEnum().WithName("نسبت");
    }
}
