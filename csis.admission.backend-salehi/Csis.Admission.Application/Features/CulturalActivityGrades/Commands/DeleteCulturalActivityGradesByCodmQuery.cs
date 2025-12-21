using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.CulturalActivityGrades.Commands;

/// <summary>
/// DeleteCulturalActivityGradesByCodmCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record DeleteCulturalActivityGradesByCodmCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteCulturalActivityGradesByCodmCommandHandler(IRepository<CulturalActivityGrade> repository)
    : IRequestHandler<DeleteCulturalActivityGradesByCodmCommand, int>
{
    public async Task<int> Handle(DeleteCulturalActivityGradesByCodmCommand request, CancellationToken cancellationToken) {
        if ( !await repository.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"رتبه فعالیت فرهنگی با شناسه {request.Id} یافت نشد.");
        }
        return request.Id;
    }
}
