using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// ویرایش موجودیت ازدواج
/// </summary>
public sealed record UpdatePersonMarriageCommand : BaseCommandDto<UpdatePersonMarriageCommand, Marriage>, IRequest
{
    /// <summary>
    /// شناسه موجودیت ازدواج
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// شناسه شوهر
    /// </summary>
    public int? HusbandPersonId { get; init; }

    /// <summary>
    /// شناسه همسر
    /// </summary>
    public int? WifePersonId { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public DateOnly? DeathDate { get; init; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public DateOnly? DivorceDate { get; init; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public DateOnly? MarriageDate { get; init; }
}

internal sealed class UpdatePersonMarriageCommandHandler : IRequestHandler<UpdatePersonMarriageCommand>
{
    private readonly IPersonMarriageRepository _marriageRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePersonMarriageCommandHandler> _logger;

    public UpdatePersonMarriageCommandHandler(
        IPersonMarriageRepository marriageRepo,
        IPersonRepository personRepo,
        IMapper mapper,
        ILogger<UpdatePersonMarriageCommandHandler> logger) {
        _marriageRepo = marriageRepo;
        _personRepo = personRepo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdatePersonMarriageCommand request, CancellationToken cancellationToken) {
        _logger.LogDebug("Updating marriage with id {id}", request.Id);

        var marriage = await _marriageRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Marriage>(request.Id);

        if ( request.HusbandPersonId.HasValue && !await _personRepo.ExistsAsync(x => x.Id == request.HusbandPersonId.Value, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.HusbandPersonId), "شوهر انتخاب شده نامعتبر است");
        }

        if ( request.WifePersonId.HasValue && !await _personRepo.ExistsAsync(x => x.Id == request.WifePersonId.Value, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.WifePersonId), "همسر انتخاب شده نامعتبر است");
        }

        _logger.LogDebug("Marriage with id {id} before update: {before}", request.Id, marriage.ToJson());

        marriage = _mapper.Map(request, marriage);

        _logger.LogDebug("Marriage with id {id} after update: {after}", request.Id, marriage.ToJson());

        await _marriageRepo.UpdateAsync(marriage, cancellationToken: cancellationToken);
    }
}
