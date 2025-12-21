using Csis.Admission.Application.Features.Elites.Dtos;

namespace Csis.Admission.Application.Features.Elites.Queries;

/// <inheritdoc/>
public sealed record GetElitesByIdQuery(int Id) : IRequest<EliteDto>;

internal sealed class GetElitesByIdQueryHandler : IRequestHandler<GetElitesByIdQuery, EliteDto>
{
    private readonly IRepository<Elite> _repo;
    public GetElitesByIdQueryHandler(IRepository<Elite> repo) {
        _repo = repo;
    }

    public async Task<EliteDto> Handle(GetElitesByIdQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetOneAsync<EliteDto>(x => x.Id == request.Id, cancellationToken: cancellationToken);
        return result;
    }
}
