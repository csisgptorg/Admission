using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// ایجاد موجودیت ازدواج جدید
/// </summary>
public sealed record CreatePersonMarriageCommand : BaseCommandDto<CreatePersonMarriageCommand, Marriage>, IRequest<int>
{
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

internal sealed class CreatePersonMarriageCommandHandler : IRequestHandler<CreatePersonMarriageCommand, int>
{
    private readonly IPersonMarriageRepository _personMarriageRepo;
    private readonly IPersonRepository _personRepo;

    public CreatePersonMarriageCommandHandler(
        IPersonMarriageRepository personMarriageRepo,
        IPersonRepository personRepo,
        ILogger<CreatePersonMarriageCommandHandler> logger) {
        _personMarriageRepo = personMarriageRepo;
        _personRepo = personRepo;
    }

    public async Task<int> Handle(CreatePersonMarriageCommand request, CancellationToken cancellationToken) {

        if ( request.HusbandPersonId.HasValue && !await _personRepo.ExistsAsync(x => x.Id == request.HusbandPersonId.Value, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.HusbandPersonId), "شوهر انتخاب شده نامعتبر است");
        }

        if ( request.WifePersonId.HasValue && !await _personRepo.ExistsAsync(x => x.Id == request.WifePersonId.Value, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.WifePersonId), "همسر انتخاب شده نامعتبر است");
        }

        var marriage = request.ToEntity();

        await _personMarriageRepo.InsertAsync(marriage, cancellationToken: cancellationToken);

        return marriage.Id;
    }
}
