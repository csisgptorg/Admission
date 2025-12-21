namespace Csis.Admission.Application.Features.StudentFriends.Commands;

/// <summary>
/// ویرایش پژوهش
/// </summary>
public sealed record DeleteStudentFriendCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteStudentFriendCommandHandler(IRepository<StudentFriend> studentFriendRepository, ILogger<DeleteStudentFriendCommandHandler> logger) : IRequestHandler<DeleteStudentFriendCommand,int>
{
    public async Task<int> Handle(DeleteStudentFriendCommand request, CancellationToken cancellationToken) {
        if ( !await studentFriendRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"دوست مورد نظر یافت نشد");
        }
        return request.Id;
    }
}
