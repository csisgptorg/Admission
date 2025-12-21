using Csis.Admission.Application.Features.Countries.Dtos;

namespace Csis.Admission.Application.Features.Countries.Queries;

/// <summary>دریافت لیست کشورها</summary>
public sealed record GetCountriesQuery() : IRequest<CountryDto[]>;

internal sealed class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, CountryDto[]>
{
    private readonly IRepository<Country,short> _repo;
    public GetCountriesQueryHandler(IRepository<Country, short> repo) {
        _repo = repo;
    }

    public async Task<CountryDto[]> Handle(GetCountriesQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<CountryDto>();
        return [.. result];
    }
}


