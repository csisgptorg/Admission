using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.CulturalActivities.Commands;

/// <summary>
/// CreateCulturalActivityCommand
/// </summary>
public sealed record CreateCulturalActivityCommand : BaseCommandDto<CreateCulturalActivityCommand, CulturalActivity>, IRequest<int>
{
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

internal sealed class CreateCulturalActivityCommandHandler(IRepository<CulturalActivity> culturalActivityRepo)
    : IRequestHandler<CreateCulturalActivityCommand, int>
{
    public async Task<int> Handle(CreateCulturalActivityCommand request, CancellationToken cancellationToken) {
        var culturalActivity = request.ToEntity();
        await culturalActivityRepo.InsertAsync(culturalActivity, cancellationToken: cancellationToken);
        return culturalActivity.Id;
    }
}
