using Csis.Admission.Application.Features.Researches.Dtos;

namespace Csis.Admission.Application.Features.Researches.Queries;

/// <summary>
/// GetResearchesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetResearchesByCodmQuery(int Codm) : IRequest<List<ResearchDto>>;

internal sealed class GetResearchesByCodmQueryHandler(IRepository<Research> researchRepo)
    : IRequestHandler<GetResearchesByCodmQuery, List<ResearchDto>>
{
    public async Task<List<ResearchDto>> Handle(GetResearchesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await researchRepo.GetAllAsync<ResearchDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Year)];
    }
}


