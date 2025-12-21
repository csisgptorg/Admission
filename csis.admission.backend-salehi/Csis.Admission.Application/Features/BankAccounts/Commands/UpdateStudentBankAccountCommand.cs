using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.BankAccounts.Commands;

/// <summary>بروز رسانی حساب بانکی</summary>
public sealed record UpdateStudentBankAccountCommand : IRequest<long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>شماره حساب بانکی</summary>
    public string BankAccountNumber { get; init; }
}

internal sealed class UpdateStudentBankAccountCommandHandler(IStudentBankAccountRepository bankAccountRepo)
    : IRequestHandler<UpdateStudentBankAccountCommand, long>
{
    public async Task<long> Handle(UpdateStudentBankAccountCommand command, CancellationToken cancellationToken) {
        var repoCommand = new UpdateStudentBankAccountNumberRepoCommand(command.Codm, command.BankAccountNumber);
        var result = await bankAccountRepo.Update(repoCommand);
        return result.Id;
    }
}
