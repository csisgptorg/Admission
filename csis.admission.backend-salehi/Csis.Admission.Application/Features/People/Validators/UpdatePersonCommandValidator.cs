using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

public sealed class UpdatePersonCommandValidator : BaseValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator() {
        RuleFor(x => x.BankAccountNumber).MaximumLength(13).WithName("شماره حساب");
        RuleFor(x => x.ShebaNumber).MaximumLength(26).WithName("شماره شبا");
        RuleFor(x => x.Mobile).NotEmpty().MaximumLength(11).WithName("تلفن همراه");
        RuleFor(x => x.Religion).IsInEnum().WithName("مذهب");
    }
}
