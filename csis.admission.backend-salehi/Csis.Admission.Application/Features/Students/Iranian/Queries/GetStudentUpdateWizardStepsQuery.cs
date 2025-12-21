using Csis.Authorization.Services;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>دریافت فرایندهای نیازمند بروز رسانی طلبه</summary>
public sealed record GetStudentUpdateWizardStepsQuery(int? Codm) : IRequest<StudentUpdateWizardStep[]>;

internal sealed class GetStudentUpdateWizardStepsQueryHandler(IStudentRepository repo, ICurrentUserService currentUser) : IRequestHandler<GetStudentUpdateWizardStepsQuery, StudentUpdateWizardStep[]>
{
    public async Task<StudentUpdateWizardStep[]> Handle(GetStudentUpdateWizardStepsQuery request, CancellationToken cancellationToken) {
        _=await Common.Utilities.SetCodm(request, currentUser);
        var visibilties= await repo.GetUpdateWizardStepsVisibilty(request.Codm.Value);
        var result= new List<StudentUpdateWizardStep>();
        if ( visibilties.PictureVisibility ) {
            result.Add(StudentUpdateWizardStep.Photo);
        }
        if ( visibilties.EmploymentVisibility) {
            result.Add(StudentUpdateWizardStep.JobIncome);
        }
        if ( visibilties.HouseVisibility ) {
            result.Add(StudentUpdateWizardStep.Housing);
        }
        if ( visibilties.AddressVisibility ) {
            result.Add(StudentUpdateWizardStep.Address);
        }

        return result.ToArray();
    }
}
