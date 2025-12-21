using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.ResearchGrades.Commands;

/// <summary>
/// DeleteResearchGradeRequestCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record DeleteResearchGradeRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteResearchGradeRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<DeleteResearchGradeRequestCommand>
{
    public async Task Handle(DeleteResearchGradeRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteResearchGrade);
        await requestService.Create(requestCommand, cancellationToken);
    }
}

