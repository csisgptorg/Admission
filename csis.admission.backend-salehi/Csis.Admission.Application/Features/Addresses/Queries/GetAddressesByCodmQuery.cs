using Csis.Admission.Application.Features.Addresses.Dtos;

namespace Csis.Admission.Application.Features.Addresses.Queries;

/// <inheritdoc/>
public sealed record GetAddressesByCodmQuery(int Codm) : IRequest<AddressDto>;

internal sealed class GetAddressesByCodmQueryHandler(IRepository<Address> repo)
    : IRequestHandler<GetAddressesByCodmQuery, AddressDto>
{
    public async Task<AddressDto> Handle(GetAddressesByCodmQuery request, CancellationToken cancellationToken) {
        var selfProjectCode = 1;
        var result = await repo.GetOneAsync<AddressDto>(x => x.Codm == request.Codm && x.ProjectCode == (short) selfProjectCode, cancellationToken: cancellationToken);
        return result;
    }
}
