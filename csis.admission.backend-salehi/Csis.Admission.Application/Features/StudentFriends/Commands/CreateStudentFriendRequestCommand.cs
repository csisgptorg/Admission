using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.StudentFriends.Commands;

/// <inheritdoc/>
public sealed record CreateStudentFriendRequestCommand : BaseCommandDto<CreateStudentFriendRequestCommand, StudentFriend>, IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public int? FriendCodm { get; init; }

    /// <inheritdoc/>
    public long? Mobile { get; init; }

    /// <inheritdoc/>
    public string FirstName { get; init; }

    /// <inheritdoc/>
    public string LastName { get; init; }
}

internal sealed class CreateStudentFriendRequestCommandHandler(IRequestService requestService, IRepository<StudentFriend> repo, ICurrentUserService currentUserService)
    : IRequestHandler<CreateStudentFriendRequestCommand, long>
{
    public async Task<long> Handle(CreateStudentFriendRequestCommand request, CancellationToken cancellationToken) {
        var codm = await currentUserService.Codm();
        if ( request.FriendCodm == codm || request.Codm == request.FriendCodm ) {
            throw new CommandValidationException("امکان ثبت طلبه به عنوان دوست برای خود, وجود ندارد");
        }

        var exists = await repo.ExistsAsync(x => x.Codm == request.Codm && x.FriendCodm == request.FriendCodm, false, cancellationToken);
        if ( exists ) {
            throw new CommandValidationException("پیش از این ثبت شده است");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateStudentFriend);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}
