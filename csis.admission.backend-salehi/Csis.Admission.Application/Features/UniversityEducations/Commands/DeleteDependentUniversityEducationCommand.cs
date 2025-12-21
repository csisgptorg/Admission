namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>
/// حذف تحصیلات دانشگاهی تکفل
/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
/// <param name="EducationId"></param>
public sealed record class DeleteDependentUniversityEducationCommand(int Codm, long DependentId, int EducationId) : IRequest<int>;
internal sealed class DeleteDependentUniversityEducationCommandHandler(IRepository<UniversityEducation> educationRepo)
    : IRequestHandler<DeleteDependentUniversityEducationCommand, int>
{
    public async Task<int> Handle(DeleteDependentUniversityEducationCommand command, CancellationToken cancellationToken) {
        await educationRepo.DeleteAsync(command.EducationId, cancellationToken: cancellationToken);
        return command.EducationId;
    }
}
