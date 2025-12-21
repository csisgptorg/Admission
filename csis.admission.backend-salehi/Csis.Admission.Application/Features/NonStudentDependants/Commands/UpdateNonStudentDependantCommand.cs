using AutoMapper;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;
using Csis.Utilities.Extensions;
using Csis.Utilities.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.NonStudentDependants.Commands;

/// <summary>
/// ویرایش موجودیت تکفل های غیرطلبه
/// </summary>
public sealed record UpdateNonStudentDependantCommand : BaseCommandDto<UpdateNonStudentDependantCommand, NonStudentDependant>, IRequest
{
    /// <summary>
    /// شناسه موجودیت تکفل های غیرطلبه
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// شناسه غیر طلبه
    /// </summary>
    public long NonStudentCodm { get; init; }

    /// <summary>
    /// فعال بودن
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// نسبت
    /// </summary>
    public DependentRelation Relationship { get; init; }

    /// <summary>
    /// شعبه
    /// </summary>
    public byte? Branch { get; init; }

    /// <summary>
    /// تاریخ ایجاد پرونده
    /// </summary>
    public DateOnly CaseCreateDate { get; init; }

    /// <summary>
    /// تاریخ غیرفعال سازی پرونده
    /// </summary>
    public DateOnly? CaseDeactiveDate { get; init; }

    /// <summary>
    /// ترتیب نسبت
    /// برای والدین صفر است
    /// </summary>
    public byte RelationshipOrder { get; init; }

    /// <summary>
    /// دلیل وضعیت فعال یا غیرفعالی
    /// </summary>
    public byte? StatusReason { get; init; }
}

internal sealed class UpdateNonStudentDependantCommandHandler : IRequestHandler<UpdateNonStudentDependantCommand>
{
    private readonly INonStudentDependantRepository _nonStudentDependantRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateNonStudentDependantCommandHandler> _logger;

    public UpdateNonStudentDependantCommandHandler(
        INonStudentDependantRepository nonStudentDependantRepo,
        IPersonRepository personRepo,
        IMapper mapper,
        ILogger<UpdateNonStudentDependantCommandHandler> logger) {
        _nonStudentDependantRepo = nonStudentDependantRepo;
        _personRepo = personRepo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateNonStudentDependantCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Updating nonStudentDependant with id {id}", request.Id);

        var nonStudentDependant = await _nonStudentDependantRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<NonStudentDependant>(request.Id);

        if ( !await _personRepo.ExistsAsync(x => x.Id == request.PersonId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.PersonId), "شخس انتخاب شده نامعتبر است");
        }

        _logger.LogDebug("NonStudentDependant with id {id} before update: {before}", request.Id, nonStudentDependant.ToJson());

        nonStudentDependant = _mapper.Map(request, nonStudentDependant);

        _logger.LogDebug("NonStudentDependant with id {id} after update: {after}", request.Id, nonStudentDependant.ToJson());

        await _nonStudentDependantRepo.UpdateAsync(nonStudentDependant, cancellationToken: cancellationToken);
    }
}
