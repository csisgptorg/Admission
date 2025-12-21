using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.TeachGrades.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.TeachGrades.Validators;

public sealed class CreateTeachGradeCommandValidator : BaseValidator<CreateTeachGradeCommand>
{
    public CreateTeachGradeCommandValidator() {
    }
}
