namespace Csis.Admission.Application.Features.Researches.Commands;

/// <summary>
/// حذف پژوهش با شناسه
/// </summary>
/// <param name="Id">شناسه پژوهش</param>
public sealed record DeleteResearchCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteResearchCommandHandler(IRepository<Research> researchRepo, ILogger<DeleteResearchCommandHandler> logger) : IRequestHandler<DeleteResearchCommand, int>
{
    public async Task<int> Handle(DeleteResearchCommand request, CancellationToken cancellationToken) {
        if ( !await researchRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"حذف پژوهش با شناسه {request.Id} ناموفق بود.");
        }

        logger.LogDebug("Research with id {id} deleted.", request.Id);
        return request.Id;
    }
}
