using Csis.Admission.Application.Features.TeachGrades.Dtos;

namespace Csis.Admission.Application.Features.TeachGrades.Queries;

/// <summary>
/// GetTeachGradesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetTeachGradesByCodmQuery(int Codm) : IRequest<List<TeachGradeDto>>;

internal sealed class GetTeachGradesByCodmQueryHandler : IRequestHandler<GetTeachGradesByCodmQuery, List<TeachGradeDto>>
{
    private readonly IRepository<TeachGrade> _reachGradeRepo;
    public GetTeachGradesByCodmQueryHandler(IRepository<TeachGrade> reachGradeRepo) {
        _reachGradeRepo = reachGradeRepo;
    }

    public async Task<List<TeachGradeDto>> Handle(GetTeachGradesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _reachGradeRepo.GetAllAsync<TeachGradeDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
