using Csis.Admission.Application.Features.SoldierStudents.Dtos;

namespace Csis.Admission.Application.Features.SoldierStudents.Queries;

/// <inheritdoc/>
public sealed record GetSoldierStudentByCodmQuery(int Codm) : IRequest<List<SoldierStudentDto>>;

internal sealed class GetSoldierStudentByPersonnelIdQueryHandler : IRequestHandler<GetSoldierStudentByCodmQuery, List<SoldierStudentDto>>
{
    private readonly IRepository<SoldierStudent> _repo;
    public GetSoldierStudentByPersonnelIdQueryHandler(IRepository<SoldierStudent> repo) {
        _repo = repo;
    }

    public async Task<List<SoldierStudentDto>> Handle(GetSoldierStudentByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<SoldierStudentDto>(x => x.Codm == request.Codm, false,cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
