using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.TeachGrades.Commands;

/// <summary>
/// DeleteTeachGradeCommand
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteTeachGradeCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteTeachGradeCommandHandler(IRepository<TeachGrade> reachGradeRepo)
    : IRequestHandler<DeleteTeachGradeCommand, int>
{
    public async Task<int> Handle(DeleteTeachGradeCommand request, CancellationToken cancellationToken) {
        if ( !await reachGradeRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"رتبه تدریس با شناسه {request.Id} یافت نشد.");
        }
        return request.Id;
    }
}
