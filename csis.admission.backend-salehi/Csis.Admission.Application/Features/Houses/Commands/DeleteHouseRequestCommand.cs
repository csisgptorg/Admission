using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Houses.Commands;

/// <summary>
/// Õ–› «ÿ·«⁄«  „”ò‰ (œ—ŒÊ«” )
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â „”ò‰</param>
public sealed record DeleteHouseRequestCommand(int Codm, int Id) : IRequest;

internal sealed class DeleteHouseRequestCommandHandler(
    IRequestService requestService,
    IRepository<House> repo,
    ILogger<DeleteHouseRequestCommandHandler> logger)
    : IRequestHandler<DeleteHouseRequestCommand>
{
    public async Task Handle(DeleteHouseRequestCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var house = await repo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (house == null)
        {
            throw new CommandValidationException($"«ÿ·«⁄«  „”ò‰ „Ê—œ ‰Ÿ— ?«›  ‰‘œ");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.DeleteHouse);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
