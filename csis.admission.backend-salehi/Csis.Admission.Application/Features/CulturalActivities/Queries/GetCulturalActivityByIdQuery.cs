using Csis.Admission.Application.Features.CulturalActivities.Dtos;

namespace Csis.Admission.Application.Features.CulturalActivities.Queries;

/// <summary>
/// GetCulturalActivityByIdQuery
/// </summary>
/// <param name="Id"></param>
public sealed record GetCulturalActivityByIdQuery(int Id) : IRequest<CulturalActivityDto>;

internal sealed class GetCulturalActivityByIdQueryHandler : IRequestHandler<GetCulturalActivityByIdQuery, CulturalActivityDto>
{
    private readonly IRepository<CulturalActivity> _culturalActivityRepo;
    public GetCulturalActivityByIdQueryHandler(IRepository<CulturalActivity> culturalActivityRepo) {
        _culturalActivityRepo = culturalActivityRepo;
    }

    public async Task<CulturalActivityDto> Handle(GetCulturalActivityByIdQuery request, CancellationToken cancellationToken) {
        return await _culturalActivityRepo.GetByIdAsync<CulturalActivityDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<CulturalActivity>(request.Id);
    }
}
