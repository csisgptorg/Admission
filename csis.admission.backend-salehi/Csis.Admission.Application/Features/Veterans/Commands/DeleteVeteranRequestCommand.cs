using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>
/// Õ–› «ÿ·«⁄«  «?À«—ê—? (œ—ŒÊ«” )
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â «?À«—ê—?</param>
public sealed record DeleteVeteranRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteVeteranRequestCommandHandler(
  IRequestService requestService,
    IRepository<Veteran> repo,
    ILogger<DeleteVeteranRequestCommandHandler> logger)
    : IRequestHandler<DeleteVeteranRequestCommand>
{
    public async Task Handle(DeleteVeteranRequestCommand request, CancellationToken cancellationToken)
    {
   // Validation
  var veteran = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (veteran == null)
  {
     throw new CommandValidationException($"«ÿ·«⁄«  «?À«—ê—? „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
  }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteVeteran);
      await requestService.Create(requestCommand, cancellationToken);
 }
}
