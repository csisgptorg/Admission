using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.ResearchGrades.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.ResearchGrades.Validators;

public sealed class UpdateResearchGradeCommandValidator : BaseValidator<UpdateResearchGradeCommand>
{
    public UpdateResearchGradeCommandValidator() {
    }
}
