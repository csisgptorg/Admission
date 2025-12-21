using Csis.Admission.Application.Features.Memorizers.Dtos;

namespace Csis.Admission.Application.Features.Memorizers.Queries;

/// <summary>حافظین</summary>
public sealed record GetDependentMemorizerByCodmQuery(int Codm) : IRequest<List<DependentMemorizerDto>>;

internal sealed class GetDependentMemorizerByCodmQueryHandler : IRequestHandler<GetDependentMemorizerByCodmQuery, List<DependentMemorizerDto>>
{
    private readonly IRepository<Memorizer> _repo;
    public GetDependentMemorizerByCodmQueryHandler(IRepository<Memorizer> repo) {
        _repo = repo;
    }

    public async Task<List<DependentMemorizerDto>> Handle(GetDependentMemorizerByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<DependentMemorizerDto>(x=>x.Codm==request.Codm, false,cancellationToken);
        return [..result.OrderByDescending(x=> x.Id)];
    }
}
