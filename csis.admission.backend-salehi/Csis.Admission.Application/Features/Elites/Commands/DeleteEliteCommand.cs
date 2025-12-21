namespace Csis.Admission.Application.Features.Elites.Commands;

/// <summary>
/// Õ–› ‰Œ»ê«‰
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â ‰Œ»ê«‰</param>
public sealed record DeleteEliteCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteEliteCommandHandler(
    IRepository<Elite> eliteRepository,
    ILogger<DeleteEliteCommandHandler> logger)
    : IRequestHandler<DeleteEliteCommand, int>
{
    public async Task<int> Handle(DeleteEliteCommand request, CancellationToken cancellationToken)
    {
await eliteRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
        return request.Id;
    }
}
