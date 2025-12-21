using Csis.Admission.Application.Features.CulturalActivities.Dtos;

namespace Csis.Admission.Application.Features.CulturalActivities.Queries;

///<inheritdoc/>
public sealed record GetCulturalActivitiesByCodmQuery(int Codm) : IRequest<List<CulturalActivityDto>>;

internal sealed class GetCulturalActivitiesByCodmQueryHandler : IRequestHandler<GetCulturalActivitiesByCodmQuery, List<CulturalActivityDto>>
{
    private readonly IRepository<CulturalActivity> _culturalActivityRepo;
    public GetCulturalActivitiesByCodmQueryHandler(IRepository<CulturalActivity> culturalActivityRepo) {
        _culturalActivityRepo = culturalActivityRepo;
    }

    public async Task<List<CulturalActivityDto>> Handle(GetCulturalActivitiesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _culturalActivityRepo.GetAllAsync<CulturalActivityDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
