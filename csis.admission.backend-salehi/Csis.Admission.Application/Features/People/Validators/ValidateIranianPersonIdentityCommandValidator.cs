using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

internal sealed class ValidateIranianPersonIdentityCommandValidator : BaseValidator<ValidateIranianPersonIdentityCommand>
{
    public ValidateIranianPersonIdentityCommandValidator()
    {
        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی نباید خالی باشد")
            .Matches("^[0-9]{10}$").WithMessage("فرمت کد ملی نامعتبر است");

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("تاریخ تولد برای شهروندان ایرانی الزامی است");
    }
}
