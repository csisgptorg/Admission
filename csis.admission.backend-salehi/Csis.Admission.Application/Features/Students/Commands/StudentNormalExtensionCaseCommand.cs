using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// ویرایش عادی پرونده
/// </summary>
public sealed record StudentNormalEditCaseCommand(int Codm, string CaseDescription) : IRequest<long>;

internal sealed class StudentNormalExtensionCaseCommandHandler(
    IStudentRepository repo,
    ICurrentUserService currentUserService)
    : IRequestHandler<StudentNormalEditCaseCommand, long>
{
    public async Task<long> Handle(StudentNormalEditCaseCommand request, CancellationToken cancellationToken) {

        var command = new StudentNormalEditCaseCommandPrc() {
            Codm = request.Codm,
            CaseDescription = request.CaseDescription,
            DataSource = DataSource.Employee,
            UserId = (await currentUserService.GetUserIdAsync()) ?? 0,
            ApplicationId = 66,
            PersonnelId = await currentUserService.PersonnelId()
        };

        var result = await repo.EditCaseCommand(command);
        return result.Id;
    }

}
