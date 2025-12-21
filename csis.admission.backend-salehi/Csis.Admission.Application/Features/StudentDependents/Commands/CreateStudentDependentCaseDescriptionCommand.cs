using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.StudentDependents.Commands;

/// <summary>
/// تغییر مشخصات پرونده ای تکفل
/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
/// <param name="CaseDescription"></param>
public sealed record CreateStudentDependentCaseDescriptionCommand(int Codm, long DependentId, string CaseDescription) : IRequest<long>;
internal sealed class CreateStudentDependentCaseDescriptionCommandHandler(
    IStudentRepository studentRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<CreateStudentDependentCaseDescriptionCommand, long>
{
    public async Task<long> Handle(CreateStudentDependentCaseDescriptionCommand command, CancellationToken cancellationToken) {
        var request = new CreateStudentDependentCaseDescriptionPrc {
            Codm = command.Codm,
            DependentId = command.DependentId,
            CaseDescription = command.CaseDescription,
            PersonnelId = await currentUserService.PersonnelId() ?? 0,
            ApplicationId = 66,
        };
        var result = await studentRepository.SetDependentCaseDescription(request);
        return result.Id;
    }
}
