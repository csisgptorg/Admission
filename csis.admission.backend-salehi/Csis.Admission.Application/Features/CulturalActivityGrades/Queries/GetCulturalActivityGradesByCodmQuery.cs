using Csis.Admission.Application.Features.CulturalActivityGrades.Dtos;

namespace Csis.Admission.Application.Features.CulturalActivityGrades.Queries;

/// <summary>
/// GetCulturalActivityGradesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetCulturalActivityGradesByCodmQuery(int Codm) : IRequest<List<CulturalActivityGradeDto>>;

internal sealed class GetCulturalActivityGradesByCodmQueryHandler(IRepository<CulturalActivityGrade> reachGradeRepo)
    : IRequestHandler<GetCulturalActivityGradesByCodmQuery, List<CulturalActivityGradeDto>>
{
    public async Task<List<CulturalActivityGradeDto>> Handle(GetCulturalActivityGradesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await reachGradeRepo.GetAllAsync<CulturalActivityGradeDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
