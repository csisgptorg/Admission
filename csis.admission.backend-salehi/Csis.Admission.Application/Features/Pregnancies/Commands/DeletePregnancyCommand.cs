namespace Csis.Admission.Application.Features.Pregnancies.Commands;

/// <inheritdoc/>
public sealed record DeletePregnancyCommand(int Id) : IRequest;

internal sealed class UpdatePregnancyCommandHandler(
    IRepository<Pregnancy> repo)
    : IRequestHandler<DeletePregnancyCommand>
{
    public async Task Handle(DeletePregnancyCommand command, CancellationToken cancellationToken) {
        if ( !await repo.DeleteAsync(command.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("حذف بارداری با خطا مواجه شد");
        }
    }
}
