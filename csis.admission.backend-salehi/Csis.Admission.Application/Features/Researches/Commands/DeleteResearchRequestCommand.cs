using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Researches.Commands;

/// <summary>
/// حذف پژوهش با شناسه
/// </summary>
/// <param name="Id">شناسه پژوهش</param>
public sealed record DeleteResearchRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteResearchRequestCommandHandler(IRequestService requestService, ILogger<DeleteResearchRequestCommandHandler> logger) : IRequestHandler<DeleteResearchRequestCommand>
{
    public async Task Handle(DeleteResearchRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteResearch);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
