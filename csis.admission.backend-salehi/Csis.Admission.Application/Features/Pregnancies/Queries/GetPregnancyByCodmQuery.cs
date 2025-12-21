using Csis.Admission.Application.Features.Pregnancies.Dtos;

namespace Csis.Admission.Application.Features.Pregnancies.Queries;

/// <inheritdoc/>
public sealed record GetPregnancyByCodmQuery(int Codm) : IRequest<List<PregnancyDto>>;

internal sealed class GetPregnancyByPersonnelIdQueryHandler : IRequestHandler<GetPregnancyByCodmQuery, List<PregnancyDto>>
{
    private readonly IRepository<Pregnancy> _repo;
    public GetPregnancyByPersonnelIdQueryHandler(IRepository<Pregnancy> repo) {
        _repo = repo;
    }

    public async Task<List<PregnancyDto>> Handle(GetPregnancyByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<PregnancyDto>(x => x.Codm == request.Codm, cancellationToken:cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
