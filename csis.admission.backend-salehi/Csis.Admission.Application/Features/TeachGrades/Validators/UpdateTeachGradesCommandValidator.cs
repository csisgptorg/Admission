using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.TeachGrades.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.TeachGrades.Validators;

public sealed class UpdateTeachGradeCommandValidator : BaseValidator<UpdateTeachGradeCommand>
{
    public UpdateTeachGradeCommandValidator() {
    }
}
