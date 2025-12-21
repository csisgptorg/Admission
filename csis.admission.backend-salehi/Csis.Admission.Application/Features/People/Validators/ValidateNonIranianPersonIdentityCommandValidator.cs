using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

internal sealed class ValidateNonIranianPersonIdentityCommandValidator : BaseValidator<ValidateNonIranianPersonIdentityCommand>
{
    public ValidateNonIranianPersonIdentityCommandValidator()
    {
        RuleFor(x => x.YektaCode)
            .NotEmpty().WithMessage("شناسه یکتا برای افراد غیرایرانی الزامی است");
    }
}
