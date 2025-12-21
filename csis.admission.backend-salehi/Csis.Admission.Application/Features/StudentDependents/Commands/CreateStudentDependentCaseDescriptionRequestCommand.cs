using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.StudentDependents.Commands;

/// <summary>/// تغییر مشخصات پرونده ای تکفل/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
/// <param name="CaseDescription"></param>
public sealed record CreateStudentDependentCaseDescriptionRequestCommand(int Codm, long DependentId, string CaseDescription) : IRequest<long>;
internal sealed class CreateStudentDependentCaseDescriptionRequestCommandHandler(
    IRepository<DependentSummary, long> studentRepository,
    IRequestService requestService)
    : IRequestHandler<CreateStudentDependentCaseDescriptionRequestCommand, long>
{
    public async Task<long> Handle(CreateStudentDependentCaseDescriptionRequestCommand command, CancellationToken cancellationToken) {
        var dependent = (await studentRepository.GetOneAsync(x => x.Id == command.DependentId)) ?? throw new CommandValidationException("تکفل مورد نظر یافت نشد.");

        var requestCommand = new CreateRequestCommand(command, RequestFlow.DirectRegistration, RequestType.CreateStudentDependentCaseDescription);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}
