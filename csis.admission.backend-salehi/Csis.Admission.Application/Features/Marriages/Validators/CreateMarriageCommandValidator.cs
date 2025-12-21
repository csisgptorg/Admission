using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.Marriages.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Marriages.Validators;

public sealed class CreateMarriageCommandValidator : BaseValidator<CreatePersonMarriageCommand>
{
    public CreateMarriageCommandValidator() {
    }
}
