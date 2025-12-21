namespace Csis.Admission.Application.Features.ImamJamaat.Commands;
public sealed record DeleteMosqueCommand(int MosqueId) : IRequest;

public sealed class DeleteMosqueCommandHandler : IRequestHandler<DeleteMosqueCommand>
{
    private readonly IRepository<Mosque> _repository;

    public DeleteMosqueCommandHandler(IRepository<Mosque> repository) {
        _repository = repository;
    }
    public async Task Handle(DeleteMosqueCommand request, CancellationToken cancellationToken) {
        if ( !await _repository.DeleteAsync(request.MosqueId, cancellationToken: cancellationToken) ) {
            throw new RecordNotFoundException<Mosque>(request.MosqueId);
        }
    }
}
