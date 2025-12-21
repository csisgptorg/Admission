using Csis.Admission.Application.Features.StudentFriends.Dtos;

namespace Csis.Admission.Application.Features.StudentFriends.Queries;

/// <inheritdoc/>
public sealed record GetStudentFriendByCodmQuery(int Codm) : IRequest<List<StudentFriendDto>>;

internal sealed class GetStudentFriendByCodmQueryHandler(IRepository<StudentFriend> repo)
    : IRequestHandler<GetStudentFriendByCodmQuery, List<StudentFriendDto>>
{
    public async Task<List<StudentFriendDto>> Handle(GetStudentFriendByCodmQuery request, CancellationToken cancellationToken) {
        var studentFriends = await repo.GetAllAsync<StudentFriendDto>(x => x.Codm == request.Codm, false, cancellationToken);
        return [.. studentFriends.OrderByDescending(x => x.Id)];
    }
}
