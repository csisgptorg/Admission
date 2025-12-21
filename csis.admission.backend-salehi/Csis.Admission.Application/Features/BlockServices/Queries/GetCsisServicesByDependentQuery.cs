using Csis.Admission.Application.Features.BlockServices.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Queries;

/// <summary>دریافت لیست خدمات تکفل</summary>
public sealed record GetCsisServicesByDependentQuery(long DependentId) : IRequest<List<CsisServiceDto>>;

internal sealed class GetCsisServicesByDependentQueryHandler(IRepository<CsisService> repo, IRepository<DependentBlockService> dependentBlockServiceRepo)
: IRequestHandler<GetCsisServicesByDependentQuery, List<CsisServiceDto>>
{
    public async Task<List<CsisServiceDto>> Handle(GetCsisServicesByDependentQuery query, CancellationToken cancellation) {
        var services = Enum.GetValues<DependentBlockServiceEnum>().Select(x => new CsisServiceDto { Id = (int) x, Title = x.GetEnumDisplayName() }).ToList();
        var excludeServices = (await dependentBlockServiceRepo.GetAllAsync(x => x.DependentId == query.DependentId, false, cancellation))
            .Select(x => x.ServiceId).ToArray();
        return services.Where(x => !excludeServices.Contains(x.Id)).ToList();
    }
}
