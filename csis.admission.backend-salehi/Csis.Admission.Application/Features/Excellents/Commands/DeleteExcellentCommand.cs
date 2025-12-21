namespace Csis.Admission.Application.Features.Excellents.Commands;

/// <summary>
/// Õ–› „„ «“?
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â „„ «“?</param>
public sealed record DeleteExcellentCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteExcellentCommandHandler(
    IRepository<Excellent> excellentRepository,
    ILogger<DeleteExcellentCommandHandler> logger)
    : IRequestHandler<DeleteExcellentCommand, int>
{
    public async Task<int> Handle(DeleteExcellentCommand request, CancellationToken cancellationToken)
    {
     await excellentRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
   return request.Id;
    }
}
