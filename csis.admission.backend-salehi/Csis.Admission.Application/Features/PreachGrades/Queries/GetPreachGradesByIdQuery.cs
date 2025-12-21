using Csis.Admission.Application.Features.PreachGrades.Dtos;

namespace Csis.Admission.Application.Features.PreachGrades.Queries;

/// <summary>
/// GetPreachGradeByIdQuery
/// </summary>
/// <param name="Id"></param>
public sealed record GetPreachGradeByIdQuery(int Id) : IRequest<PreachGradeDto>;

internal sealed class GetPreachGradeByIdQueryHandler : IRequestHandler<GetPreachGradeByIdQuery, PreachGradeDto>
{
    private readonly IRepository<PreachGrade> _preachGradeRepo;
    public GetPreachGradeByIdQueryHandler(IRepository<PreachGrade> preachGradeRepo) {
        _preachGradeRepo = preachGradeRepo;
    }

    public async Task<PreachGradeDto> Handle(GetPreachGradeByIdQuery request, CancellationToken cancellationToken) {
        return await _preachGradeRepo.GetByIdAsync<PreachGradeDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<PreachGrade>(request.Id);
    }
}
