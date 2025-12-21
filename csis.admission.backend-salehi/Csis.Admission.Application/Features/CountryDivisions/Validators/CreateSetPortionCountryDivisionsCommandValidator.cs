using Csis.Admission.Application.Features.CountryDivisions.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CountryDivisions.Validators;

public sealed class CreateSetPortionCountryDivisionsCommandValidator : BaseValidator<CreateSetPortionCountryDivisionsCommand>
{
    public CreateSetPortionCountryDivisionsCommandValidator() {
        RuleFor(x=> x.Title)
            .NotEmpty()
            .MaximumLength(1000)
            .WithName("عنوان");

        RuleFor(x=> x.CityId)
            .NotEmpty()
            .WithName("شناسه شهر");
    }
}
