using Csis.Admission.Application.Features.Towns.Dtos;

namespace Csis.Admission.Application.Features.Towns.Queries;

/// <summary>
/// دریافت لیست شهرها بر اساس استان
/// </summary>
/// <param name="PortionId"></param>
public sealed record GetTownsQuery(short? PortionId) : IRequest<TownDto[]>;

internal sealed class GetTownsQueryHandler : IRequestHandler<GetTownsQuery, TownDto[]>
{
    private readonly IRepository<Town, short> _repo;
    public GetTownsQueryHandler(IRepository<Town, short> repo) {
        _repo = repo;
    }

    public async Task<TownDto[]> Handle(GetTownsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<TownDto>(x => request.PortionId == null || x.PortionId == request.PortionId);
        return result.ToArray();
    }
}
