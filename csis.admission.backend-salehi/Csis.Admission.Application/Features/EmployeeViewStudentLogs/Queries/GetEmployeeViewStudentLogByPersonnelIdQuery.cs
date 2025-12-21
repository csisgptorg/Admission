using Csis.Authorization.Services;
using Csis.Admission.Application.Features.ViewLogs.Dtos;

namespace Csis.Admission.Application.Features.EmployeeViewStudentLogs.Queries;

/// <summary>·«ê „‘«ÂœÂ «ÿ·«⁄«  ÿ·»Â  Ê”ÿ ò«—„‰œ</summary>
public sealed record GetEmployeeViewStudentLogByPersonnelIdQuery : IRequest<List<EmployeeViewStudentLogDto>>;

internal sealed class GetEmployeeViewStudentLogByPersonnelIdQueryHandler : 
    IRequestHandler<GetEmployeeViewStudentLogByPersonnelIdQuery, List<EmployeeViewStudentLogDto>>
{
    private readonly IRepository<EmployeeViewStudentLog, long> _repo;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    public GetEmployeeViewStudentLogByPersonnelIdQueryHandler(IRepository<EmployeeViewStudentLog, long> repo, ICsisAuthenticatedUserService authenticatedUserService) {
        _repo = repo;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<List<EmployeeViewStudentLogDto>> Handle(GetEmployeeViewStudentLogByPersonnelIdQuery request, CancellationToken cancellationToken) {
        var personnelId = await _authenticatedUserService.GetPersonnelIdAsync();
        var result = await _repo.GetAllAsync<EmployeeViewStudentLogDto>(x => x.PersonnelId == personnelId, false,cancellationToken);
        return result;
    }
}
