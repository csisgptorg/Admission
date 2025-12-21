using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

/// <summary>
/// اعتبارسنجی دستور اعتبارسنجی شماره همراه شخص
/// </summary>
public sealed class ValidatePersonMobileCommandValidator : BaseValidator<ValidatePersonMobileCommand>
{
    public ValidatePersonMobileCommandValidator()
    {
        RuleFor(x => x.NationalCode)
            .NotEmpty()
            .WithMessage("کد ملی الزامی است.")
            .Must(NationalCodeHelper.IsValidNationalCode)
            .WithMessage("کد ملی وارد شده معتبر نیست.");

        RuleFor(x => x.Mobile)
            .NotEmpty()
            .WithMessage("شماره همراه الزامی است.")
            .Length(11)
            .WithMessage("شماره همراه باید ۱۱ رقم باشد.")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره همراه باید با ۰۹ شروع شود و شامل ۱۱ رقم باشد.");
    }
}
