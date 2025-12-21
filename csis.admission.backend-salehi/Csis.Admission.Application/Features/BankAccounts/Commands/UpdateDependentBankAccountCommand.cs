using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.BankAccounts.Commands;

/// <summary>بروز رسانی حساب بانکی</summary>
public sealed record UpdateDependentBankAccountCommand : IRequest<long>
{
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>شماره حساب بانکی</summary>
    public string BankAccountNumber { get; init; }
}

internal sealed class UpdateDependentBankAccountCommandHandler(IStudentBankAccountRepository bankAccountRepo)
    : IRequestHandler<UpdateDependentBankAccountCommand, long>
{
    public async Task<long> Handle(UpdateDependentBankAccountCommand command, CancellationToken cancellationToken) {
        var repoCommand = new UpdateDependentBankAccountNumberRepoCommand(command.DependentId, command.Codm, command.BankAccountNumber);
        var result = await bankAccountRepo.UpdateDependent(repoCommand);
        return result.Id;
    }
}
