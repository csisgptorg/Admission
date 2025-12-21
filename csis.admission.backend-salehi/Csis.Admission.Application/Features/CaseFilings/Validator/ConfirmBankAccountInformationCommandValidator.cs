using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class ConfirmBankAccountInformationCommandValidator : AbstractValidator<CreateBankAccountInformationCommand>
{
    public ConfirmBankAccountInformationCommandValidator() {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("شناسه الزامی است.");
        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("شماره حساب بانکی الزامی است.")
            .MaximumLength(24).WithMessage("شماره حساب بانکی نمی تواند بیشتر از 24 کاراکتر باشد.");
    }
}
