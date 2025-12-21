using Csis.Admission.Application.Features.CountryDivisions.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CountryDivisions.Validators;

public sealed class CreateSetRuralCountryDivisionsCommandValidator : BaseValidator<CreateSetRuralCountryDivisionsCommand>
{
    public CreateSetRuralCountryDivisionsCommandValidator() {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(1000)
            .WithName("عنوان");

        RuleFor(x => x.PortionId)
            .NotEmpty()
            .WithName("شناسه بخش");
    }
}
