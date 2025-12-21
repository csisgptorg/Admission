using Csis.Authorization.Services;
using Csis.Admission.Application.Features.ViewLogs.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.EmployeeViewStudentLogs.Queries;

/// <summary>·«ê „‘«ÂœÂ «ÿ·«⁄«  ÿ·»Â  Ê”ÿ ò«—„‰œ</summary>
public sealed record EmployeeLastViewStudentLogDtoByPersonnelIdQuery : IRequest<EmployeeLastViewStudentLogDto[]>;

internal sealed class GetLastEmployeeViewStudentLogByPersonnelIdQueryHandler :
    IRequestHandler<EmployeeLastViewStudentLogDtoByPersonnelIdQuery, EmployeeLastViewStudentLogDto[]>
{
    private readonly IStudentRepository _repo;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    public GetLastEmployeeViewStudentLogByPersonnelIdQueryHandler(IStudentRepository repo, ICsisAuthenticatedUserService authenticatedUserService) {
        _repo = repo;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<EmployeeLastViewStudentLogDto[]> Handle(EmployeeLastViewStudentLogDtoByPersonnelIdQuery request, CancellationToken cancellationToken) {
        var personnelId = await _authenticatedUserService.GetPersonnelIdAsync();
        var result = await _repo.GetEmployeeLastViewStudentLogByPersonnelId(personnelId.Value);
        return result;
    }
}
