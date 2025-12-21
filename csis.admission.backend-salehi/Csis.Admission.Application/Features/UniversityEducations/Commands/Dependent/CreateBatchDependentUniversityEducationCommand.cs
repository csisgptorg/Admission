namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>تحصیلات دانشگاهی</summary>
public sealed record class CreateBatchDependentUniversityEducationCommand(long DependentId, CreateDependentUniversityEducationCommand[] Commands) : IRequest<int[]>;
internal sealed class CreateBatchDependentUniversityEducationCommandHandler(IRepository<UniversityEducation> universityRepo, IRepository<DependentSummary,long> dependentRepo)
    : IRequestHandler<CreateBatchDependentUniversityEducationCommand, int[]>
{
    public async Task<int[]> Handle(CreateBatchDependentUniversityEducationCommand command, CancellationToken cancellationToken) {

        var universityEducations = command.Commands.Select(x => x.ToEntity()).ToList();
        var dependent = await dependentRepo.GetByIdAsync(command.DependentId, false, cancellationToken: cancellationToken);
        universityEducations.ForEach(x=>x.Codm=dependent.Codm);

        await universityRepo.BulkInsertAsync(universityEducations, true, cancellationToken);
        return universityEducations.Select(x => x.Id).ToArray();
    }
}
