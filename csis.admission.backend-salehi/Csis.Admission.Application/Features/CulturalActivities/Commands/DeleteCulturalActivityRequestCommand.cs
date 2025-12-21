using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.CulturalActivities.Commands;

/// <summary>
/// DeleteCulturalActivityCommand
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteCulturalActivityRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteCulturalActivityRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<DeleteCulturalActivityRequestCommand>
{
    public async Task Handle(DeleteCulturalActivityRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteCulturalActivity);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
