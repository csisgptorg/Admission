using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.NonStudentDependants.Commands;

/// <summary>
/// حذف موجودیت تکفل های غیرطلبه با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت تکفل های غیرطلبه</param>
public sealed record DeleteNonStudentDependantCommand(int Id) : IRequest;

internal sealed class DeleteNonStudentDependantCommandHandler : IRequestHandler<DeleteNonStudentDependantCommand>
{
    private readonly INonStudentDependantRepository _nonStudentDependantRepo;
    private readonly ILogger<DeleteNonStudentDependantCommandHandler> _logger;

    public DeleteNonStudentDependantCommandHandler(INonStudentDependantRepository nonStudentDependantRepo, ILogger<DeleteNonStudentDependantCommandHandler> logger) {
        _nonStudentDependantRepo = nonStudentDependantRepo;
        _logger = logger;
    }

    public async Task Handle(DeleteNonStudentDependantCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Deleting nonStudentDependant with id {id}", request.Id);

        if ( !await _nonStudentDependantRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new RecordNotFoundException<NonStudentDependant>(request.Id);
        }

        _logger.LogDebug("NonStudentDependant with id {id} deleted.", request.Id);
    }
}
