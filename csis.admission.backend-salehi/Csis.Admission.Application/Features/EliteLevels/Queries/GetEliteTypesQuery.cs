using Csis.Admission.Application.Features.EliteLevels.Dtos;

namespace Csis.Admission.Application.Features.EliteLevels.Queries;

/// <summary>دریافت لیست انواع نخبگانی</summary>
public sealed record GetEliteLevelsQuery : IRequest<EliteLevelDto[]>;

internal sealed class GetEliteLevelsQueryHandler : IRequestHandler<GetEliteLevelsQuery, EliteLevelDto[]>
{
    private readonly IRepository<EliteLevel, short> _repo;
    public GetEliteLevelsQueryHandler(IRepository<EliteLevel, short> repo) {
        _repo = repo;
    }

    public async Task<EliteLevelDto[]> Handle(GetEliteLevelsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<EliteLevelDto>(cancellationToken: cancellationToken);
        return [.. result];
    }
}
