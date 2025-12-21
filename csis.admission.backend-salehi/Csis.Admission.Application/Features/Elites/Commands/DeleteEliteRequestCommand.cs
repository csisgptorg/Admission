using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Elites.Commands;

/// <summary>
/// Õ–› ‰Œ»ê«‰ (œ—ŒÊ«” )
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â ‰Œ»ê«‰</param>
public sealed record DeleteEliteRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteEliteRequestCommandHandler(
    IRequestService requestService,
    IRepository<Elite> repo,
    ILogger<DeleteEliteRequestCommandHandler> logger)
    : IRequestHandler<DeleteEliteRequestCommand>
{
    public async Task Handle(DeleteEliteRequestCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var elite = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (elite == null)
        {
            throw new CommandValidationException($"‰Œ»ê«‰ „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteElite);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
