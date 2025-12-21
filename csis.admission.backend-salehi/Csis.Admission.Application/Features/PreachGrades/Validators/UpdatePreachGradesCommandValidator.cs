using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.PreachGrades.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.PreachGrades.Validators;

public sealed class UpdatePreachGradeCommandValidator : BaseValidator<UpdatePreachGradeCommand>
{
    public UpdatePreachGradeCommandValidator() {
    }
}
