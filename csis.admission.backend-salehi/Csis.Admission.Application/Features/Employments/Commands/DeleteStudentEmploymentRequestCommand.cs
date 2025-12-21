using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>
/// Õ–› «‘ €«· ÿ·»Â
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â «‘ €«·</param>
public sealed record DeleteStudentEmploymentRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteStudentEmploymentRequestCommandHandler(
    IRequestService requestService,
    IRepository<StudentEmployment> repo,
    ILogger<DeleteStudentEmploymentRequestCommandHandler> logger)
    : IRequestHandler<DeleteStudentEmploymentRequestCommand>
{
    public async Task Handle(DeleteStudentEmploymentRequestCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var employment = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (employment == null)
        {
            throw new CommandValidationException($"«‘ €«· „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteStudentEmployment);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
