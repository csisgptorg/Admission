using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.NonStudentDependants.Commands;

/// <summary>
/// ایجاد موجودیت تکفل های غیرطلبه جدید
/// </summary>
public sealed record CreateNonStudentDependantCommand : BaseCommandDto<CreateNonStudentDependantCommand, NonStudentDependant>, IRequest<int>
{
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

internal sealed class CreateNonStudentDependantCommandHandler : IRequestHandler<CreateNonStudentDependantCommand, int>
{
    private readonly IPersonRepository _personRepo;
    private readonly INonStudentDependantRepository _nonStudentDependantRepo;
    private readonly ILogger<CreateNonStudentDependantCommandHandler> _logger;

    public CreateNonStudentDependantCommandHandler(
        INonStudentDependantRepository nonStudentDependantRepo,
        IPersonRepository personRepo,
        ILogger<CreateNonStudentDependantCommandHandler> logger) {
        _nonStudentDependantRepo = nonStudentDependantRepo;
        _personRepo = personRepo;
        _logger = logger;
    }

    public async Task<int> Handle(CreateNonStudentDependantCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Mapping create nonStudentDependant command: {command}", request.ToJson());

        if ( !await _personRepo.ExistsAsync(x => x.Id == request.PersonId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.PersonId), "شخس انتخاب شده نامعتبر است");
        }

        var nonStudentDependant = request.ToEntity();

        _logger.LogDebug("Creating nonStudentDependant: {nonStudentDependant}", nonStudentDependant.ToJson());

        await _nonStudentDependantRepo.InsertAsync(nonStudentDependant, cancellationToken: cancellationToken);

        _logger.LogDebug("NonStudentDependant created with id {id}", nonStudentDependant.Id);
        return nonStudentDependant.Id;
    }
}
