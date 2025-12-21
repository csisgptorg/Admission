using Csis.Admission.Application.Features.ResearchGrades.Dtos;

namespace Csis.Admission.Application.Features.ResearchGrades.Queries;

/// <summary>
/// GetResearchGradeByIdQuery
/// </summary>
/// <param name="Id"></param>
public sealed record GetResearchGradeByIdQuery(int Id) : IRequest<ResearchGradeDto>;

internal sealed class GetResearchGradeByIdQueryHandler : IRequestHandler<GetResearchGradeByIdQuery, ResearchGradeDto>
{
    private readonly IRepository<ResearchGrade> _researchGradeRepo;
    public GetResearchGradeByIdQueryHandler(IRepository<ResearchGrade> researchGradeRepo) {
        _researchGradeRepo = researchGradeRepo;
    }

    public async Task<ResearchGradeDto> Handle(GetResearchGradeByIdQuery request, CancellationToken cancellationToken) {
        return await _researchGradeRepo.GetByIdAsync<ResearchGradeDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ResearchGrade>(request.Id);
    }
}
