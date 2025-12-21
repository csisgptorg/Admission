namespace Csis.Admission.Application.Features.Famouses.Commands;

/// <summary>
/// حذف مشهور با شناسه
/// </summary>
/// <param name="Id">شناسه مشهور</param>
public sealed record DeleteFamousCommand(int Codm,int Id) : IRequest<int>;

internal sealed class DeleteFamousCommandHandler(IRepository<Famous> famousRepo, ILogger<DeleteFamousCommandHandler> logger) : IRequestHandler<DeleteFamousCommand,int>
{
    public async Task<int> Handle(DeleteFamousCommand request, CancellationToken cancellationToken) {
        if ( !await famousRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException( $"طلبه مشهور با شناسه {request.Id} یافت نشد." );
        }

        logger.LogDebug("Famous with id {id} deleted.", request.Id);
        return request.Id;
    }
}

