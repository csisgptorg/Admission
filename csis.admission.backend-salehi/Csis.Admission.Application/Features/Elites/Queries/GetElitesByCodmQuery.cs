using Csis.Admission.Application.Features.Elites.Dtos;

namespace Csis.Admission.Application.Features.Elites.Queries;

/// <inheritdoc/>
public sealed record GetElitesByCodmQuery(int Codm) : IRequest<List<EliteDto>>;

internal sealed class GetElitesByCodmQueryHandler : IRequestHandler<GetElitesByCodmQuery, List<EliteDto>>
{
    private readonly IRepository<Elite> _repo;
    public GetElitesByCodmQueryHandler(IRepository<Elite> repo) {
        _repo = repo;
    }

    public async Task<List<EliteDto>> Handle(GetElitesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<EliteDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return result;
    }
}
