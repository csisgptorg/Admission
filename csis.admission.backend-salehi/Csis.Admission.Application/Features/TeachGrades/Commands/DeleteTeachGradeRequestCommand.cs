using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.TeachGrades.Commands;

/// <summary>
/// DeleteTeachGradeRequestCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record DeleteTeachGradeRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteTeachGradeRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<DeleteTeachGradeRequestCommand>
{
    public async Task Handle(DeleteTeachGradeRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteTeachGrade);
        await requestService.Create(requestCommand, cancellationToken);

    }
}
