using System.Linq.Expressions;

namespace Csis.Admission.Application.Features.SoldierStudents.Commands;

/// <summary>
/// DeleteSoldierStudentCommand
/// </summary>
/// <param name="Id"></param>
/// <param name="Codm"></param>
public sealed record DeleteSoldierStudentCommand(int Id, int? Codm = null) : IRequest;

internal sealed class DeleteSoldierStudentCommandHandler : IRequestHandler<DeleteSoldierStudentCommand>
{
    private readonly IRepository<SoldierStudent> _repo;
    public DeleteSoldierStudentCommandHandler(IRepository<SoldierStudent> repo) {
        _repo = repo;
    }

    public async Task Handle(DeleteSoldierStudentCommand request, CancellationToken cancellationToken) {

        Expression<Func<SoldierStudent, bool>> pridacate =x=> (request.Codm == null && x.Id == request.Id) || (x.Id == request.Id && x.Codm == request.Codm);
        var entity = await _repo.GetOneAsTrackingAsync(pridacate, false, cancellationToken) ?? throw new RecordNotFoundException<SoldierStudent>(request.Id);
        await _repo.DeleteAsync(entity, true, cancellationToken);
    }
}
