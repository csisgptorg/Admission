using Csis.Admission.Application.Features.Provinces.Dtos;

namespace Csis.Admission.Application.Features.Provinces.Queries;

/// <summary>دریافت لیست استان ها</summary>
public sealed record GetProvincesQuery() : IRequest<ProvinceDto[]>;

internal sealed class GetProvincesQueryQueryHandler : IRequestHandler<GetProvincesQuery, ProvinceDto[]>
{
    private readonly IRepository<Province,short> _repo;
    public GetProvincesQueryQueryHandler(IRepository<Province, short> repo) {
        _repo = repo;
    }

    public async Task<ProvinceDto[]> Handle(GetProvincesQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<ProvinceDto>();
        return result.ToArray();
    }
}
