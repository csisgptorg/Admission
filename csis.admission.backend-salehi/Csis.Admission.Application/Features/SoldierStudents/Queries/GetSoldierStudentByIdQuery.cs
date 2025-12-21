using Csis.Admission.Application.Features.SoldierStudents.Dtos;

namespace Csis.Admission.Application.Features.SoldierStudents.Queries;

/// <inheritdoc/>
public sealed record GetSoldierStudentByIdQuery(int Id,int? Codm=null) : IRequest<SoldierStudentDto>;

internal sealed class GetSoldierStudentByIdQueryHandler : IRequestHandler<GetSoldierStudentByIdQuery, SoldierStudentDto>
{
    private readonly IRepository<SoldierStudent> _repo;
    public GetSoldierStudentByIdQueryHandler(IRepository<SoldierStudent> repo) {
        _repo = repo;
    }

    public async Task<SoldierStudentDto> Handle(GetSoldierStudentByIdQuery request, CancellationToken cancellationToken) {

        return await _repo.GetOneAsync<SoldierStudentDto>(x=>(request.Codm==null && x.Id==request.Id) || (x.Id==request.Id && x.Codm==request.Codm),false,cancellationToken)
            ?? throw new RecordNotFoundException<SoldierStudent>(request.Id);
    }
}
