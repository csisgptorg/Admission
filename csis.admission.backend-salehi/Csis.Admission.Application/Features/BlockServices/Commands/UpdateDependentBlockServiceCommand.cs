using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Commands;

/// <summary>ویرایش</summary>
public sealed record UpdateDependentBlockServiceCommand : BaseCommandDto<UpdateDependentBlockServiceCommand, DependentBlockService>, IRequest
{
    /// <summary>شناسه</summary>
    public int Id { get; init; }

    /// <summary>علت</summary>
    public string Reason { get; init; }
}

internal sealed class UpdateDependentBlockServiceCommandHandler(IRepository<DependentBlockService> repo)
    : IRequestHandler<UpdateDependentBlockServiceCommand>
{
    public async Task Handle(UpdateDependentBlockServiceCommand command, CancellationToken cancellation) {

        var dependentBlockService= await repo.GetByIdAsTrackingAsync(command.Id, false, cancellation)
                    ?? throw new CommandValidationException("رکورد یافت نشد.");

        dependentBlockService = command.ToEntity(dependentBlockService);
        await repo.UpdateAsync(dependentBlockService, true, cancellation);
    }
}
