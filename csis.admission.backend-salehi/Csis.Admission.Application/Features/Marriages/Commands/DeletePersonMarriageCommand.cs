using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// حذف موجودیت ازدواج با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت ازدواج</param>
public sealed record DeletePersonMarriageCommand(int Id) : IRequest;

internal sealed class DeleteMarriageCommandHandler : IRequestHandler<DeletePersonMarriageCommand>
{
    private readonly IPersonMarriageRepository _personMarriageRepo;
    private readonly ILogger<DeleteMarriageCommandHandler> _logger;

    public DeleteMarriageCommandHandler(IPersonMarriageRepository marriageRepo, ILogger<DeleteMarriageCommandHandler> logger) {
        _personMarriageRepo = marriageRepo;
        _logger = logger;
    }

    public async Task Handle(DeletePersonMarriageCommand request, CancellationToken cancellationToken) {
        
        _logger.LogDebug("Deleting marriage with id {id}", request.Id);

        if ( !await _personMarriageRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new RecordNotFoundException<Marriage>(request.Id);
        }

        _logger.LogDebug("Marriage with id {id} deleted.", request.Id);
    }
}
