using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>محاسبه خودکار وضعیت پرونده تکفل</summary>
public sealed record UpdateDependentCaseActiveEmployeeCommand(int Codm, long DependentId, DependentDeActiveReasonEnum? DependentDeActiveReason, DependentActiveReasonEnum? DependentActiveReason) : IRequest<long>;

internal sealed partial class UpdateDependentCaseActiveEmployeeCommandHandler(ICurrentUserService currentUserService, IStudentRepository studentRepository) : IRequestHandler<UpdateDependentCaseActiveEmployeeCommand, long>
{
    public async Task<long> Handle(UpdateDependentCaseActiveEmployeeCommand request, CancellationToken cancellationToken) {
       
        if ( request.DependentActiveReason == null && request.DependentDeActiveReason != null) {
            var command = new Common.Models.Repository.UpdateStudentDependentCaseDeActiveStatusPrc {
                Codm = request.Codm,
                DependentId = request.DependentId,
                DeActiveReason = request.DependentDeActiveReason.Value,
                ApplicationId = 66,
                DataSource = DataSource.Employee,
                PersonnelId = await currentUserService.PersonnelId() ?? 0,
                UserId = await currentUserService.GetUserIdAsync() ?? 0
            };

            _ = await studentRepository.UpdateDependentCaseDeActiveStatus(command);

        }

        if ( request.DependentDeActiveReason == null && request.DependentActiveReason != null) {
            var activateCommand = new Common.Models.Repository.UpdateStudentDependentCaseActiveStatusPrc {
                Codm = request.Codm,
                DependentId = request.DependentId,
                ActiveReason = request.DependentActiveReason.Value,
                ApplicationId = 66,
                DataSource = DataSource.Employee,
                PersonnelId = await currentUserService.PersonnelId() ?? 0,
                UserId = await currentUserService.GetUserIdAsync() ?? 0
            };

            var activateResult = await studentRepository.UpdateDependentCaseActiveStatus(activateCommand);
        }

        return request.DependentId;
    }
}

