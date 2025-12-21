using Csis.Admission.Application.Features.Cities.Dtos;
using Csis.Admission.Application.Features.Rurals.Dtos;
using Csis.Admission.Application.Features.Towns.Dtos;

namespace Csis.Admission.Application.Features.Cities.Queries;

/// <summary>دریافت لیست شهرها</summary>
public sealed record GetCitiesQuery(short? ProvinceId) : IRequest<CityDto[]>;

internal sealed class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, CityDto[]>
{
    private readonly IRepository<City,short> _repo;
    public GetCitiesQueryHandler(IRepository<City, short> repo) {
        _repo = repo;
    }

    public async Task<CityDto[]> Handle(GetCitiesQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<CityDto>(x => request.ProvinceId == null || x.ProvinceId == request.ProvinceId);
        return result.ToArray();
    }
}


