namespace Csis.Admission.Application.Features.PreachGrades.Commands;

/// <summary>
/// DeletePreachGradeCommand
/// </summary>
/// <param name="Id"></param>
public sealed record DeletePreachGradeCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeletePreachGradeCommandHandler(IRepository<PreachGrade> preachGradeRepo)
    : IRequestHandler<DeletePreachGradeCommand, int>
{
    public async Task<int> Handle(DeletePreachGradeCommand request, CancellationToken cancellationToken) {
        if ( !await preachGradeRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"رتبه تبلیغی با شناسه {request.Id} یافت نشد.");
        }
        return request.Id;
    }
}

