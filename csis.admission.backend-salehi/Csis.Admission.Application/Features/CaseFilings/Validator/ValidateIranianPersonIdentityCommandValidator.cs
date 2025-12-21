using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class ValidateIdentityCommandValidator : BaseValidator<ValidateIdentityCommand>
{
    public ValidateIdentityCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty() .WithMessage("شناسه نباید خالی باشد");
    }
}
