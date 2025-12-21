using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Famouses.Commands;

/// <summary>
/// حذف مشهور با شناسه
/// </summary>
/// <param name="Id">شناسه مشهور</param>
public sealed record DeleteFamousRequestCommand(int Codm, int Id) : IRequest;
internal sealed class DeleteFamousRequestCommandHandler(IRequestService requestService, ILogger<DeleteFamousRequestCommandHandler> logger) : IRequestHandler<DeleteFamousRequestCommand>
{
    public async Task Handle(DeleteFamousRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteFamous);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
