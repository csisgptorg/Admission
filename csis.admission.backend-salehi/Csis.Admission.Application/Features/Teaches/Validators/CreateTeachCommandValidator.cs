using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.Teaches.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Teaches.Validators;

public sealed class CreateTeachCommandValidator : BaseValidator<CreateTeachCommand>
{
    public CreateTeachCommandValidator() {
    }
}
