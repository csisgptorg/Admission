using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>
/// ویرایش وضعیت فعال بودن تکفل در پرونده پذیرش
/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
/// <param name="DeActiveReason"></param>
/// <param name="ActiveReason"></param>
public sealed record class UpdateDependentCaseActiveStatusSeniorCommand(int Codm, long DependentId, DependentDeActiveReasonEnum? DeActiveReason, DependentActiveReasonEnum? ActiveReason) : IRequest<long>;

internal sealed class UpdateDependentCaseDeActiveHandler(IStudentRepository studentRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateDependentCaseActiveStatusSeniorCommand, long>
{

    public async Task<long> Handle(UpdateDependentCaseActiveStatusSeniorCommand request, CancellationToken cancellationToken) {
        ProcedureResultDto result = new();


        if ( request.DeActiveReason.HasValue ) {
            var command = new UpdateStudentDependentCaseDeActiveStatusPrc {
                Codm = request.Codm,
                DependentId = request.DependentId,
                DeActiveReason = request.DeActiveReason.Value,
                ApplicationId = 66,
                PersonnelId = await currentUserService.PersonnelId() ?? 0,
                DataSource = DataSource.Employee,
                UserId = await currentUserService.GetUserIdAsync() ?? 0
            };
            result = await studentRepository.UpdateDependentCaseDeActiveStatus(command);
        } else if
            ( request.ActiveReason.HasValue ) {
            var command = new UpdateStudentDependentCaseActiveStatusPrc {
                Codm = request.Codm,
                DependentId = request.DependentId,
                ActiveReason = request.ActiveReason.Value,
                ApplicationId = 66,
                PersonnelId = await currentUserService.PersonnelId() ?? 0,
                DataSource = DataSource.Employee,
                UserId = await currentUserService.GetUserIdAsync() ?? 0
            };
            result = await studentRepository.UpdateDependentCaseActiveStatus(command);
        }

        return result.Id;

    }
}
