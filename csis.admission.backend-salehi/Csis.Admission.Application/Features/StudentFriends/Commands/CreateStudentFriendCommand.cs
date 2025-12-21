using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.StudentFriends.Commands;

/// <inheritdoc/>
public sealed record CreateStudentFriendCommand : BaseCommandDto<CreateStudentFriendCommand, StudentFriend>, IRequest<int>
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

internal sealed class CreateStudentFriendCommandHandler(IRepository<StudentFriend> repo)
    : IRequestHandler<CreateStudentFriendCommand, int>
{
    public async Task<int> Handle(CreateStudentFriendCommand request, CancellationToken cancellationToken) {
        var entity = request.ToEntity();
        await repo.InsertAsync(entity, true, cancellationToken);
        return entity.Id;
    }
}
