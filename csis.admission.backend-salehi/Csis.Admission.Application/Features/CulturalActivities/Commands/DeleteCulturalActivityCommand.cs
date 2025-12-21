namespace Csis.Admission.Application.Features.CulturalActivities.Commands;

/// <summary>
/// DeleteCulturalActivityCommand
/// </summary>
/// <param name="Id"></param>
/// <param name="Codm"></param>
public sealed record DeleteCulturalActivityCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteCulturalActivityCommandHandler(IRepository<CulturalActivity> culturalActivityRepo)
    : IRequestHandler<DeleteCulturalActivityCommand, int>
{
    public async Task<int> Handle(DeleteCulturalActivityCommand request, CancellationToken cancellationToken) {
        if ( !await culturalActivityRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"فعالیت فرهنگی مورد نظر با شناسه {request.Id} یافت نشد");
        }
        return request.Id;
    }
}
