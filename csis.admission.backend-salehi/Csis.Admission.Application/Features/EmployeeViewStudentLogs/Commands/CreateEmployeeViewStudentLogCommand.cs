using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>لاگ مشاهده اطلاعات طلبه توسط کارمند</summary>
public sealed record CreateEmployeeViewStudentLogCommand (int codm):IRequest<long>;

internal sealed class CreateEmployeeViewStudentLogCommandHandler : IRequestHandler<CreateEmployeeViewStudentLogCommand, long>
{
    private readonly IRepository<EmployeeViewStudentLog,long> _repo;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    public CreateEmployeeViewStudentLogCommandHandler(IRepository<EmployeeViewStudentLog, long> repo, ICsisAuthenticatedUserService authenticatedUserService) {
        _repo = repo;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<long> Handle(CreateEmployeeViewStudentLogCommand request,CancellationToken cancellationToken) {

        var employeeViewStudent = new EmployeeViewStudentLog { Codm=request.codm};
        employeeViewStudent.PersonnelId=(await _authenticatedUserService.GetPersonnelIdAsync()).Value;
        employeeViewStudent.Date=DateTime.Now.ToPersianInteger();
        employeeViewStudent.Time=DateTime.Now.TimeOfDay;

        await _repo.InsertAsync(employeeViewStudent, true, cancellationToken);
        return employeeViewStudent.Id;
    }
}
