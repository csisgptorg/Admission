using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.ResearchGrades.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.ResearchGrades.Validators;

public sealed class CreateResearchGradeCommandValidator : BaseValidator<CreateResearchGradeCommand>
{
    public CreateResearchGradeCommandValidator() {
    }
}
