using Csis.Admission.Application.Features.Excellents.Dtos;

namespace Csis.Admission.Application.Features.Excellents.Queries;

/// <summary>
/// GetExcellentByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetExcellentsByCodmQuery(int Codm) : IRequest<List<ExcellentDto>>;

internal sealed class GetExcellentsByCodmQueryHandler : IRequestHandler<GetExcellentsByCodmQuery, List<ExcellentDto>>
{
    private readonly IRepository<Excellent> _excellentRepo;
    public GetExcellentsByCodmQueryHandler(IRepository<Excellent> excellentRepo) {
        _excellentRepo = excellentRepo;
    }

    public async Task<List<ExcellentDto>> Handle(GetExcellentsByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _excellentRepo.GetAllAsync<ExcellentDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [..result.OrderByDescending(x=> x.EducationYearId)];
    }
}
