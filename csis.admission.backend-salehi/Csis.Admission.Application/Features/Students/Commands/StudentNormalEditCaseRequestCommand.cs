using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// ویرایش عادی پرونده
/// </summary>
public sealed record StudentNormalEditCaseRequestCommand(int Codm, string CaseDescription) : IRequest;

internal sealed class StudentNormalExtensionCaseRequestCommandHandler(
    IStudentRepository repo,
    IRequestService requestService,
    ICurrentUserService currentUserService)
    : IRequestHandler<StudentNormalEditCaseRequestCommand>
{
    public async Task Handle(StudentNormalEditCaseRequestCommand request, CancellationToken cancellationToken) {
        var isEmployee = await currentUserService.IsEmployee();
        if ( !isEmployee ) {
            throw new CommandValidationException("فقط کارمندان مجاز به انجام این عملیات می باشند.");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.StudentNormalEditCase);
        var requestResult = await requestService.Create(requestCommand, cancellationToken);
    }

}
