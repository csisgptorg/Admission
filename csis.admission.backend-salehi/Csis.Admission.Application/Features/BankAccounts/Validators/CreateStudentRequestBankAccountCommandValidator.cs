using Csis.Admission.Application.Features.BankAccounts.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.BankAccounts.Validators;

public sealed  class CreateStudentRequestBankAccountCommandValidator : BaseValidator<UpdateStudentBankAccountRequestCommand>
{
    public CreateStudentRequestBankAccountCommandValidator() {
        RuleFor(x => x.BankAccountNumber).NotEmpty().Length(13).WithName("شماره حساب");

    }
}
