using Csis.Admission.Application.Features.Portions.Dtos;

namespace Csis.Admission.Application.Features.Portions.Queries;

/// <summary>لیست</summary>
public sealed record GetPortionsQuery(short? CityId) : IRequest<PortionDto[]>;

internal sealed class GetPortionsQueryHandler : IRequestHandler<GetPortionsQuery, PortionDto[]>
{
    private readonly IRepository<Portion,short> _repo;
    public GetPortionsQueryHandler(IRepository<Portion, short> repo) {
        _repo = repo;
    }

    public async Task<PortionDto[]> Handle(GetPortionsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<PortionDto>(x => request.CityId == null || x.CityId == request.CityId);
        return result.ToArray();
    }
}
