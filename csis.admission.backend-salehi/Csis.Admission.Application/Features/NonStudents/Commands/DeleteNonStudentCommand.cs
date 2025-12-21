using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.NonStudents.Commands;

/// <summary>
/// حذف موجودیت غیر طلبه با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت غیر طلبه</param>
public sealed record DeleteNonStudentCommand(long Id) : IRequest;

internal sealed class DeleteNonStudentCommandHandler : IRequestHandler<DeleteNonStudentCommand>
{
    private readonly INonStudentRepository _nonStudentRepo;
    private readonly ILogger<DeleteNonStudentCommandHandler> _logger;

    public DeleteNonStudentCommandHandler(INonStudentRepository nonStudentRepo, ILogger<DeleteNonStudentCommandHandler> logger) {
        _nonStudentRepo = nonStudentRepo;
        _logger = logger;
    }

    public async Task Handle(DeleteNonStudentCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Deleting nonStudent with id {id}", request.Id);

        if ( !await _nonStudentRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new RecordNotFoundException<NonStudent>(request.Id);
        }

        _logger.LogDebug("NonStudent with id {id} deleted.", request.Id);
    }
}
