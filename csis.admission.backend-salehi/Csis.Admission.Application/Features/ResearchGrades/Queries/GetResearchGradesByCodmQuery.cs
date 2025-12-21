using Csis.Admission.Application.Features.ResearchGrades.Dtos;

namespace Csis.Admission.Application.Features.ResearchGrades.Queries;

/// <summary>
/// GetResearchGradesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetResearchGradesByCodmQuery(int Codm) : IRequest<List<ResearchGradeDto>>;

internal sealed class GetResearchGradesByCodmQueryHandler : IRequestHandler<GetResearchGradesByCodmQuery, List<ResearchGradeDto>>
{
    private readonly IRepository<ResearchGrade> _researchGradeRepo;
    public GetResearchGradesByCodmQueryHandler(IRepository<ResearchGrade> researchGradeRepo) {
        _researchGradeRepo = researchGradeRepo;
    }

    public async Task<List<ResearchGradeDto>> Handle(GetResearchGradesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _researchGradeRepo.GetAllAsync<ResearchGradeDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
