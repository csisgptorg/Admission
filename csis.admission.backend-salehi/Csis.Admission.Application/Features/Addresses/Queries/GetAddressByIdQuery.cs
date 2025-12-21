using Csis.Admission.Application.Features.Addresses.Dtos;

namespace Csis.Admission.Application.Features.Addresses.Queries;

/// <inheritdoc/>
public sealed record GetAddressByIdQuery(int Id) : IRequest<AddressDto>;

internal sealed class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressDto>
{
    private readonly IRepository<Address> _repo;
    public GetAddressByIdQueryHandler(IRepository<Address> repo) {
        _repo = repo;
    }

    public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken) {
        return await _repo.GetByIdAsync<AddressDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Address>(request.Id);
    }
}
