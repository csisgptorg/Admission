using Csis.Authorization.Services;
using Csis.Admission.Application.Features.BankAccounts.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.BankAccounts.Queries;

/// <inheritdoc/>
public sealed record GetFamilyBankAccountsByCodmQuery(int Codm) : IRequest<FamilyBankAccountDto[]>;

internal sealed class GetFamilyBankAccountsByCodmHandler(IStudentBankAccountRepository repo, ICurrentUserService currentUser) 
    : IRequestHandler<GetFamilyBankAccountsByCodmQuery, FamilyBankAccountDto[]>
{
    public async Task<FamilyBankAccountDto[]> Handle(GetFamilyBankAccountsByCodmQuery query, CancellationToken cancellationToken) {
        _=await Common.Utilities.SetCodm(query, currentUser);
        var result = await repo.GetFamiliesByCodm(query.Codm);

        var khadamatCards = await repo.GetFamiliesKhadamatCardsByCodm(query.Codm);
        foreach ( var item in result ) {
            item.KhadamatCardNumber = khadamatCards.SingleOrDefault(x=>x.DependentId.GetValueOrDefault()==item.DependentId.GetValueOrDefault())?.CardNumber;
        }

        return result;
    }
}
