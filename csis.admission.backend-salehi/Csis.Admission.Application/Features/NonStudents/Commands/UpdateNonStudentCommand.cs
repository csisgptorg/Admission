using AutoMapper;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using Csis.Utilities.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.NonStudents.Commands;

/// <summary>
/// ویرایش موجودیت غیر طلبه
/// </summary>
public sealed record UpdateNonStudentCommand : BaseCommandDto<UpdateNonStudentCommand, NonStudent, long>, IRequest
{
    /// <summary>
    /// شناسه موجودیت غیر طلبه
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// نمایندگی
    /// </summary>
    public byte? Agency { get; init; }

    /// <summary>
    /// شعبه
    /// </summary>
    public byte? Branch { get; init; }

    /// <summary>
    /// تاریخ مسدودی پرونده
    /// </summary>
    public DateOnly? CaseBlockDate { get; init; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public DateOnly CaseCreateDate { get; init; }

    /// <summary>
    /// تاریخ انقضا پرونده
    /// </summary>
    public DateOnly? CaseExpireDate { get; init; }

    /// <summary>
    /// وضعیت
    /// </summary>
    public NonStudentStatus Status { get; init; }

    /// <summary>
    /// نوع غیر طلبه
    /// </summary>
    public NonStudentType? Type { get; init; }
}

internal sealed class UpdateNonStudentCommandHandler : IRequestHandler<UpdateNonStudentCommand>
{
    private readonly INonStudentRepository _nonStudentRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateNonStudentCommandHandler> _logger;

    public UpdateNonStudentCommandHandler(
        INonStudentRepository nonStudentRepo,
        IPersonRepository personRepo,
        IMapper mapper,
        ILogger<UpdateNonStudentCommandHandler> logger) {
        _nonStudentRepo = nonStudentRepo;
        _personRepo = personRepo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateNonStudentCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Updating nonStudent with id {id}", request.Id);

        var nonStudent = await _nonStudentRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<NonStudent>(request.Id);

        if ( !await _personRepo.ExistsAsync(x => x.Id == request.PersonId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.PersonId), "شخس انتخاب شده نامعتبر است");
        }

        _logger.LogDebug("NonStudent with id {id} before update: {before}", request.Id, nonStudent.ToJson());

        nonStudent = _mapper.Map(request, nonStudent);

        _logger.LogDebug("NonStudent with id {id} after update: {after}", request.Id, nonStudent.ToJson());

        await _nonStudentRepo.UpdateAsync(nonStudent, cancellationToken: cancellationToken);
    }
}
