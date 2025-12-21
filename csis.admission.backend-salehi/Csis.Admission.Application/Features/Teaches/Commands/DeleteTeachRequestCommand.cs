using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Teaches.Commands;

/// <summary>
/// حذف سابقه تدریس برای طلبه
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteTeachRequestCommand(int Codm, int Id) : IRequest;
internal sealed class DeleteTeachRequestCommandHandler(IRequestService requestService, ICsisAuthenticatedUserService authenticatedUserService)
    : IRequestHandler<DeleteTeachRequestCommand>
{
    public async Task Handle(DeleteTeachRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteTeach);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
