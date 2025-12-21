using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.Preaches.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Preaches.Validators;

public sealed class UpdatePreachCommandValidator : BaseValidator<UpdatePreachCommand>
{
    public UpdatePreachCommandValidator() {
    }
}
