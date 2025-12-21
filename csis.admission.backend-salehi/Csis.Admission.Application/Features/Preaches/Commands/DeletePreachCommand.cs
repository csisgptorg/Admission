namespace Csis.Admission.Application.Features.Preaches.Commands;

/// <summary>
/// DeletePreachCommand
/// </summary>
/// <param name="Id"></param>
public sealed record DeletePreachCommand(int Codm, int Id) : IRequest<long>;

internal sealed class DeletePreachCommandHandler(IRepository<Preach> preachRepo) : IRequestHandler<DeletePreachCommand, long>
{
    public async Task<long> Handle(DeletePreachCommand request, CancellationToken cancellationToken) {
        if ( !await preachRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"تبلیغ با شناسه {request.Id} یافت نشد.");
        }
        return request.Id;
    }
}

