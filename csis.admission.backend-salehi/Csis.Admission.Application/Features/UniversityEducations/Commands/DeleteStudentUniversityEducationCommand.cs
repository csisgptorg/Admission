namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>
/// حذف تحصیلات دانشگاهی
/// </summary>
/// <param name="Codm"></param>
/// <param name="EducationId"></param>
public sealed record class DeleteStudentUniversityEducationCommand(int Codm, int EducationId) : IRequest<int>;
internal sealed class DeleteStudentUniversityEducationCommandHandler(IRepository<UniversityEducation> educationRepo)
    : IRequestHandler<DeleteStudentUniversityEducationCommand, int>
{
    public async Task<int> Handle(DeleteStudentUniversityEducationCommand command, CancellationToken cancellationToken) {
        await educationRepo.DeleteAsync(command.EducationId, cancellationToken: cancellationToken);
        return command.EducationId;
    }
}
