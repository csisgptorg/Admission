using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.BlockedServices.Commands;

/// <summary>
/// CreateStudentCaseBlockCommand
/// </summary>
/// <param name="Codm"></param>
/// <param name="CaseBlockReasonId"></param>
public sealed record CreateStudentCaseBlockCommand(int Codm, List<CaseBlockReason> CaseBlockReasonId) : IRequest<long>;
internal sealed class CreateStudentCaseBlockCommandHandler : IRequestHandler<CreateStudentCaseBlockCommand, long>
{
    private readonly IStudentRepository _repo;
    private readonly ICurrentUserService _currentUserService;

    public CreateStudentCaseBlockCommandHandler(IStudentRepository repo,ICurrentUserService currentUserService) {
        _repo = repo;
        _currentUserService = currentUserService;
    }
    public async Task<long> Handle(CreateStudentCaseBlockCommand request, CancellationToken cancellationToken) {
        var command = new SetStudentBlockedRepoCommand {
            Codm = request.Codm,
            BlockReasons = string.Join(",", request.CaseBlockReasonId.Select(x => ((int) x).ToString())),
            UserId = await _currentUserService.GetUserIdAsync() ?? 0,
            PersonnelId = await _currentUserService.PersonnelId(),
            ApplicationId = 66,
            DataSource = DataSource.Employee
        };

        var result = await _repo.SetStudentBlocked(command);
        return result.Id;
    }
}
