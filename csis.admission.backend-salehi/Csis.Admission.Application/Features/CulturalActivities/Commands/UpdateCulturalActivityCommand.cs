using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.CulturalActivities.Commands;

/// <summary>
/// UpdateCulturalActivityCommand
/// </summary>
public sealed record UpdateCulturalActivityCommand : BaseCommandDto<UpdateCulturalActivityCommand, CulturalActivity>, IRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نوع مدیریت فرهنگی
    /// </summary>
    public CulturalKind Kind { get; set; }

    /// <summary>
    /// سایر انواع
    /// </summary>
    public string OtherKind { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public int Year { get; set; }
}

internal sealed class UpdateCulturalActivityCommandHandler : IRequestHandler<UpdateCulturalActivityCommand>
{
    private readonly IRepository<CulturalActivity> _culturalActivityRepo;
    public UpdateCulturalActivityCommandHandler(IRepository<CulturalActivity> culturalActivityRepo) {
        _culturalActivityRepo = culturalActivityRepo;
    }

    public async Task Handle(UpdateCulturalActivityCommand request, CancellationToken cancellationToken) {
        var culturalActivity = await _culturalActivityRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<CulturalActivity>(request.Id);

        request.ToEntity(culturalActivity);
        await _culturalActivityRepo.UpdateAsync(culturalActivity, true,cancellationToken);
    }
}
