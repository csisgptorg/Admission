namespace Csis.Admission.Application.Features.ResearchGrades.Commands;

/// <summary>
/// DeleteResearchGradeCommand
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteResearchGradeCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteResearchGradeCommandHandler(IRepository<ResearchGrade> researchGradeRepo)
    : IRequestHandler<DeleteResearchGradeCommand, int>
{
    public async Task<int> Handle(DeleteResearchGradeCommand request, CancellationToken cancellationToken) {
        if ( !await researchGradeRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"رتبه پژوهشی با شناسه {request.Id} یافت نشد.");
        }
        return request.Id;
    }
}
