using Csis.Admission.Application.Features.TeachGrades.Dtos;

namespace Csis.Admission.Application.Features.TeachGrades.Queries;

/// <summary>
/// GetTeachGradeByIdQuery
/// </summary>
/// <param name="Id"></param>
public sealed record GetTeachGradeByIdQuery(int Id) : IRequest<TeachGradeDto>;

internal sealed class GetTeachGradeByIdQueryHandler : IRequestHandler<GetTeachGradeByIdQuery, TeachGradeDto>
{
    private readonly IRepository<TeachGrade> _reachGradeRepo;
    public GetTeachGradeByIdQueryHandler(IRepository<TeachGrade> reachGradeRepo) {
        _reachGradeRepo = reachGradeRepo;
    }

    public async Task<TeachGradeDto> Handle(GetTeachGradeByIdQuery request, CancellationToken cancellationToken) {
        return await _reachGradeRepo.GetByIdAsync<TeachGradeDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<TeachGrade>(request.Id);
    }
}
