using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.BlockedServices.Commands;

/// <summary>
/// CreateStudentCaseUnblockCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record CreateStudentCaseUnblockCommand(int Codm) : IRequest<long>;
internal sealed class CreateStudentCaseUnblockCommandHandler : IRequestHandler<CreateStudentCaseUnblockCommand, long>
{
    private readonly IStudentRepository _repo;
    private readonly ICurrentUserService _currentUserService;

    public CreateStudentCaseUnblockCommandHandler(IStudentRepository repo,ICurrentUserService currentUserService) {
        _repo = repo;
        _currentUserService = currentUserService;
    }
    public async Task<long> Handle(CreateStudentCaseUnblockCommand request, CancellationToken cancellationToken) {
        var command = new SetStudentUnBlockedRepoCommand {
            Codm = request.Codm,
             ApplicationId = 66,
             PersonnelId = await _currentUserService.PersonnelId(),
             DataSource = DataSource.Employee,
            UserId = await _currentUserService.GetUserIdAsync() ?? 0
        };

        var result = await _repo.SetStudentUnblocked(command);
        return result.Id;
    }
}
