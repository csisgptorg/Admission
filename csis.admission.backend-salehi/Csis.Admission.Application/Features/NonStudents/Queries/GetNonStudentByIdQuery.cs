using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.NonStudents.Dtos;
using Csis.Admission.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.NonStudents.Queries;

/// <summary>
/// دریافت موجودیت غیر طلبه با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت غیر طلبه</param>
public sealed record GetNonStudentByIdQuery(long Id) : IRequest<NonStudentDto>;

internal sealed class GetNonStudentByIdQueryHandler : IRequestHandler<GetNonStudentByIdQuery, NonStudentDto>
{
    private readonly INonStudentRepository _nonStudentRepo;
    private readonly ILogger<GetNonStudentByIdQueryHandler> _logger;

    public GetNonStudentByIdQueryHandler(INonStudentRepository nonStudentRepo, ILogger<GetNonStudentByIdQueryHandler> logger) {
        _nonStudentRepo = nonStudentRepo;
        _logger = logger;
    }

    public async Task<NonStudentDto> Handle(GetNonStudentByIdQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Getting nonStudent with id {id}", request.Id);

        return await _nonStudentRepo.GetByIdAsync<NonStudentDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<NonStudent>(request.Id);
    }
}
