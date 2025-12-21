using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.StudentFriends.Commands;

/// <summary>
/// حذف دوست
/// </summary>
public sealed record DeleteStudentFriendRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteStudentFriendRequestCommandHandler(IRequestService requestService, ILogger<DeleteStudentFriendRequestCommandHandler> logger) : IRequestHandler<DeleteStudentFriendRequestCommand>
{
    public async Task Handle(DeleteStudentFriendRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteStudentFriend);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
