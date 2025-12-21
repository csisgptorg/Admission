using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// تمدید پرونده دستی
/// </summary>
public sealed record ManualStudentExtensionCaseCommand(int Codm, List<int> CaseValidityReasonId, string CaseValidityDate) : IRequest<long>;

internal sealed class ManualStudentExtensionCaseCommandHandler(
    IStudentRepository repo,
    ICurrentUserService currentUserService)
    : IRequestHandler<ManualStudentExtensionCaseCommand, long>
{
    public async Task<long> Handle(ManualStudentExtensionCaseCommand request, CancellationToken cancellationToken) {


        var command = new ManualStudentExtensionCaseCommandPrc() {
            Codm = request.Codm,
            CaseValidityReasonList = string.Join(",", request.CaseValidityReasonId),
            CaseValidityDate = request.CaseValidityDate.StringDateToInt().Value,
            DataSource = DataSource.Employee,
            UserId = (await currentUserService.GetUserIdAsync()) ?? 0,
            ApplicationId = 66,
            PersonnelId = await currentUserService.PersonnelId()
        };

        var result = await repo.ExtensionCaseCommand(command);
        return result.Id;
    }

}
