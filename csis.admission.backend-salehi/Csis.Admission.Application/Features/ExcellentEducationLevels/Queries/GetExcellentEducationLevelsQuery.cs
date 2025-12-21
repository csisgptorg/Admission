using Csis.Admission.Application.Features.ExcellentEducationLevels.Dtos;

namespace Csis.Admission.Application.Features.ExcellentEducationLevels.Queries;

/// <summary>دریافت لیست سطوح تحصیلی</summary>
public sealed record GetExcellentEducationLevelsQuery : IRequest<ExcellentEducationLevelDto[]>;

internal sealed class GetExcellentEducationLevelsQueryHandler : IRequestHandler<GetExcellentEducationLevelsQuery, ExcellentEducationLevelDto[]>
{
    private readonly IRepository<ExcellentEducationLevel, short> _repo;
    public GetExcellentEducationLevelsQueryHandler(IRepository<ExcellentEducationLevel, short> repo) {
        _repo = repo;
    }

    public async Task<ExcellentEducationLevelDto[]> Handle(GetExcellentEducationLevelsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<ExcellentEducationLevelDto>(cancellationToken:cancellationToken);
        return [.. result];
    }
}

