using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.CulturalActivities.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CulturalActivities.Validators;

public sealed class UpdateCulturalActivityCommandValidator : BaseValidator<UpdateCulturalActivityCommand>
{
    public UpdateCulturalActivityCommandValidator() {
    }
}
