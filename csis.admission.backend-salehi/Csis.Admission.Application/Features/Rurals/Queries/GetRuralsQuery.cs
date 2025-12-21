using Csis.Admission.Application.Features.Rurals.Dtos;

namespace Csis.Admission.Application.Features.Rurals.Queries;

/// <summary>
/// دریافت لیست روستاها بر اساس استان
/// </summary>
/// <param name="PortionId"></param>
public sealed record GetRuralsQuery(short? PortionId) : IRequest<RuralDto[]>;

internal sealed class GetRuralsQueryHandler : IRequestHandler<GetRuralsQuery, RuralDto[]>
{
    private readonly IRepository<Rural, short> _repo;
    public GetRuralsQueryHandler(IRepository<Rural, short> repo) {
        _repo = repo;
    }

    public async Task<RuralDto[]> Handle(GetRuralsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<RuralDto>(x => request.PortionId == null || x.PortionId == request.PortionId);
        return result.ToArray();
    }
}
