namespace Csis.Admission.Application.Features.Memorizers.Commands;

/// <summary>
/// Õ–› Õ«›Ÿ
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â Õ«›Ÿ</param>
public sealed record DeleteMemorizerCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteMemorizerCommandHandler(
    IRepository<Memorizer> memorizerRepository,
    ILogger<DeleteMemorizerCommandHandler> logger)
    : IRequestHandler<DeleteMemorizerCommand, int>
{
    public async Task<int> Handle(DeleteMemorizerCommand request, CancellationToken cancellationToken)
    {
        await memorizerRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
        return request.Id;
    }
}
