namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>تحصیلات دانشگاهی</summary>
public sealed record class CreateBatchStudentUniversityEducationCommand(int Codm,CreateStudentUniversityEducationCommand[] Commands) : IRequest<int[]>;
internal sealed class CreateBatchStudentUniversityEducationCommandHandler(IRepository<UniversityEducation> universityRepo)
    : IRequestHandler<CreateBatchStudentUniversityEducationCommand, int[]>
{
    public async Task<int[]> Handle(CreateBatchStudentUniversityEducationCommand command, CancellationToken cancellationToken) {

        var universityEducations = command.Commands.Select(x => x.ToEntity()).ToList();
        await universityRepo.BulkInsertAsync(universityEducations, cancellationToken: cancellationToken);

        return universityEducations.Select(x=>x.Id).ToArray();
    }
}
