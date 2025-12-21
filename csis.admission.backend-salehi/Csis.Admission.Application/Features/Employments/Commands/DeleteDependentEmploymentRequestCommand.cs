using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>
/// Õ–› «‘ €«·  ò›·
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â «‘ €«·</param>
/// <param name="DependentId">‘‰«”Â  ò›·</param>
public sealed record DeleteDependentEmploymentRequestCommand(int Codm, int Id, long DependentId) : IRequest;

internal sealed class DeleteDependentEmploymentRequestCommandHandler(
    IRequestService requestService,
    IRepository<DependentEmployment> repo,
    ILogger<DeleteDependentEmploymentRequestCommandHandler> logger)
 : IRequestHandler<DeleteDependentEmploymentRequestCommand>
{
    public async Task Handle(DeleteDependentEmploymentRequestCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var employment = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
  if (employment == null)
        {
     throw new CommandValidationException($"«‘ €«·  ò›· „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
   }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteDependentEmployment);
 await requestService.Create(requestCommand, cancellationToken);
    }
}
