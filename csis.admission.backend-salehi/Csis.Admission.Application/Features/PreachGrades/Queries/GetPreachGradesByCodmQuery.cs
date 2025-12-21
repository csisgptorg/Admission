using Csis.Admission.Application.Features.PreachGrades.Dtos;

namespace Csis.Admission.Application.Features.PreachGrades.Queries;

/// <summary>
/// GetPreachGradesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetPreachGradesByCodmQuery(int Codm) : IRequest<List<PreachGradeDto>>;

internal sealed class GetPreachGradesByCodmQueryHandler : IRequestHandler<GetPreachGradesByCodmQuery, List<PreachGradeDto>>
{
    private readonly IRepository<PreachGrade> _preachGradeRepo;
    public GetPreachGradesByCodmQueryHandler(IRepository<PreachGrade> preachGradeRepo) {
        _preachGradeRepo = preachGradeRepo;
    }

    public async Task<List<PreachGradeDto>> Handle(GetPreachGradesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _preachGradeRepo.GetAllAsync<PreachGradeDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
