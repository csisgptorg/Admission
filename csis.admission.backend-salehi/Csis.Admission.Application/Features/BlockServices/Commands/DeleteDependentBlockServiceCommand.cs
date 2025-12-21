namespace Csis.Admission.Application.Features.BlockServices.Commands;

/// <summary>حذف</summary>
public sealed record DeleteDependentBlockServiceCommand(int Id) : IRequest;

internal sealed class DeleteDependentBlockServiceCommandHandler(IRepository<DependentBlockService, int> repo) 
    : IRequestHandler<DeleteDependentBlockServiceCommand> {     public async Task Handle(DeleteDependentBlockServiceCommand command, CancellationToken cancellation) {
        if ( !await repo.DeleteAsync(command.Id, true,cancellation) ) {             throw new CommandValidationException("رکورد یافت نشد.");         }     } } 