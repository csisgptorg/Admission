using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Preaches.Commands;

/// <summary>
/// DeletePreachRequestCommand
/// </summary>
/// <param name="Id"></param>
public sealed record DeletePreachRequestCommand(int Codm, int Id) : IRequest;
internal sealed class DeletePreachRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<DeletePreachRequestCommand>
{
    public async Task Handle(DeletePreachRequestCommand request, CancellationToken cancellationToken) {
        
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeletePreach);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
