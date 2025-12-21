using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Excellents.Commands;

/// <summary>
/// Õ–› „„ «“? (œ—ŒÊ«” )
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â „„ «“?</param>
public sealed record DeleteExcellentRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteExcellentRequestCommandHandler(
    IRequestService requestService,
    IRepository<Excellent> repo,
    ILogger<DeleteExcellentRequestCommandHandler> logger)
    : IRequestHandler<DeleteExcellentRequestCommand>
{
    public async Task Handle(DeleteExcellentRequestCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var excellent = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (excellent == null)
        {
            throw new CommandValidationException($"„„ «“? „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteExcellent);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
