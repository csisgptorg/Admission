using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.PreachGrades.Commands;

/// <summary>
/// DeletePreachGradeRequestCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record DeletePreachGradeRequestCommand(int Codm, int Id) : IRequest;
internal sealed class DeletePreachGradeRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<DeletePreachGradeRequestCommand>
{
    public async Task Handle(DeletePreachGradeRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeletePreachGrade);
        await requestService.Create(requestCommand, cancellationToken);

    }
}

