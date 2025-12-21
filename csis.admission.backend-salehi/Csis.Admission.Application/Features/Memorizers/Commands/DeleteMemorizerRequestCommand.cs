using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Memorizers.Commands;

/// <summary>
/// Õ–› Õ«›Ÿ (œ—ŒÊ«” )
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â Õ«›Ÿ</param>
public sealed record DeleteMemorizerRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteMemorizerRequestCommandHandler(
    IRequestService requestService,
    IRepository<Memorizer> repo,
    ILogger<DeleteMemorizerRequestCommandHandler> logger)
    : IRequestHandler<DeleteMemorizerRequestCommand>
{
    public async Task Handle(DeleteMemorizerRequestCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var memorizer = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (memorizer == null)
        {
            throw new CommandValidationException($"Õ«›Ÿ „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteMemorizer);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
