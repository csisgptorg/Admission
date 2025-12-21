using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// حذف موجودیت شخص با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت شخص</param>
public sealed record DeletePersonCommand(int Id) : IRequest;

internal sealed class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _personRepo;
    private readonly ILogger<DeletePersonCommandHandler> _logger;
     
    public DeletePersonCommandHandler(IPersonRepository personRepo, ILogger<DeletePersonCommandHandler> logger) {
        _personRepo = personRepo;
        _logger = logger;
    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Deleting person with id {id}", request.Id);

        if ( !await _personRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"شخص با شناسه {request.Id} یافت نشد.");
        }

        _logger.LogDebug("Person with id {id} deleted.", request.Id);
    }
}
