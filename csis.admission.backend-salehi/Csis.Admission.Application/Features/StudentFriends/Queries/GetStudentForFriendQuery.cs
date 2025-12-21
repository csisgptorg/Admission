using Csis.Admission.Application.Features.StudentFriends.Dtos;

namespace Csis.Admission.Application.Features.StudentFriends.Queries;

/// <inheritdoc/>
public sealed record GetStudentForFriendQuery(int Codm,string Mobile) : IRequest<StudentForFriendDto>;

internal sealed class GetStudentFamilyMobilesByCodmQueryHandler(IRepository<StudentSummary> repo)
    : IRequestHandler<GetStudentForFriendQuery, StudentForFriendDto>
{
    public async Task<StudentForFriendDto> Handle(GetStudentForFriendQuery request, CancellationToken cancellationToken) {
        var friend = await repo.GetOneAsync<StudentForFriendDto>(x => x.Codm == request.Codm && x.Mobile == request.Mobile, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("طلبه ای با این مشخصات پیدا نشد.");
        return friend;
    }
}
