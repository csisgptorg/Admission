using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.NonStudentDependants.Dtos;
using Csis.Admission.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.NonStudentDependants.Queries;

/// <summary>
/// دریافت موجودیت تکفل های غیرطلبه با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت تکفل های غیرطلبه</param>
public sealed record GetNonStudentDependantByIdQuery(int Id) : IRequest<NonStudentDependantDto>;

internal sealed class GetNonStudentDependantByIdQueryHandler : IRequestHandler<GetNonStudentDependantByIdQuery, NonStudentDependantDto>
{
    private readonly INonStudentDependantRepository _nonStudentDependantRepo;
    private readonly ILogger<GetNonStudentDependantByIdQueryHandler> _logger;

    public GetNonStudentDependantByIdQueryHandler(INonStudentDependantRepository nonStudentDependantRepo, ILogger<GetNonStudentDependantByIdQueryHandler> logger) {
        _nonStudentDependantRepo = nonStudentDependantRepo;
        _logger = logger;
    }

    public async Task<NonStudentDependantDto> Handle(GetNonStudentDependantByIdQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Getting nonStudentDependant with id {id}", request.Id);

        return await _nonStudentDependantRepo.GetByIdAsync<NonStudentDependantDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<NonStudentDependant>(request.Id);
    }
}
