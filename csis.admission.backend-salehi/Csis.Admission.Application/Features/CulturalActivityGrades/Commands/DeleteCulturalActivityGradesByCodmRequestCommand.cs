using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.CulturalActivityGrades.Commands;

/// <summary>
/// DeleteCulturalActivityGradesByCodmRequestCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record DeleteCulturalActivityGradesByCodmRequestCommand(int Codm, int Id) : IRequest;
internal sealed class DeleteCulturalActivityGradesByCodmRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<DeleteCulturalActivityGradesByCodmRequestCommand>
{
    public async Task Handle(DeleteCulturalActivityGradesByCodmRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteCulturalActivityGrade);
        await requestService.Create(requestCommand, cancellationToken);

    }
}
