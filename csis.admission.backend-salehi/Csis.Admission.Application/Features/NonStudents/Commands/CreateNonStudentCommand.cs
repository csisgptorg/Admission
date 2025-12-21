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
/// ایجاد موجودیت غیر طلبه جدید
/// </summary>
public sealed record CreateNonStudentCommand : BaseCommandDto<CreateNonStudentCommand, NonStudent, long>, IRequest<long>
{
    /// <summary>
    /// شناسه شخس
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// نمایندگی
    /// </summary>
    public byte Agency { get; init; }

    /// <summary>
    /// شعبه
    /// </summary>
    public byte Branch { get; init; }

    /// <summary>
    /// وضعیت
    /// </summary>
    public NonStudentStatus Status { get; init; }

    /// <summary>
    /// نوع غیر طلبه
    /// </summary>
    public NonStudentType? Type { get; init; }
}

internal sealed class CreateNonStudentCommandHandler : IRequestHandler<CreateNonStudentCommand, long>
{
    private readonly INonStudentRepository _nonStudentRepo;
    private readonly IPersonRepository _personRepo;
    private readonly ILogger<CreateNonStudentCommandHandler> _logger;

    public CreateNonStudentCommandHandler(
        INonStudentRepository nonStudentRepo,
        IPersonRepository personRepo,
        ILogger<CreateNonStudentCommandHandler> logger) {
        _nonStudentRepo = nonStudentRepo;
        _personRepo = personRepo;
        _logger = logger;
    }

    public async Task<long> Handle(CreateNonStudentCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Mapping create nonStudent command: {command}", request.ToJson());

        if ( !await _personRepo.ExistsAsync(x => x.Id == request.PersonId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.PersonId), "شخس انتخاب شده نامعتبر است");
        }

        var nonStudent = request.ToEntity();

        _logger.LogDebug("Creating nonStudent: {nonStudent}", nonStudent.ToJson());

        await _nonStudentRepo.InsertAsync(nonStudent, cancellationToken: cancellationToken);

        _logger.LogDebug("NonStudent created with id {id}", nonStudent.Id);
        return nonStudent.Id;
    }
}
