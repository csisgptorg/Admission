using Csis.Admission.Application.Features.BankAccounts.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.BankAccounts.Validators;

public sealed class CreateDependentRequestBankAccountCommandValidator : BaseValidator<UpdateDependentBankAccountRequestCommand>
{
    public CreateDependentRequestBankAccountCommandValidator() {
        RuleFor(x => x.BankAccountNumber).NotEmpty().Length(13).WithName("شماره حساب");
        RuleFor(x => x.DependentId).NotEmpty().WithName("شناسه تکفل");
    }
}
