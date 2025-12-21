using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.BlockServices.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Queries;

/// <summary>دریافت لیست خدمات طلبه</summary>
public sealed record GetCsisServicesByCodmQuery(int Codm) : IRequest<List<CsisServiceDto>>;

internal sealed class GetCsisServicesQueryHandler(IRepository<CsisService> repo, IRepository<StudentBlockService> studentBlockServiceRepo)
    : IRequestHandler<GetCsisServicesByCodmQuery, List<CsisServiceDto>>
{
    public async Task<List<CsisServiceDto>> Handle(GetCsisServicesByCodmQuery query, CancellationToken cancellation) {
        var services = Enum.GetValues<StudentBlockServiceEnum>().Select(x => new CsisServiceDto { Id = (int) x, Title = x.GetEnumDisplayName() }).ToList();
        var excludeServices = (await studentBlockServiceRepo.GetAllAsync(x => x.Codm == query.Codm, false, cancellation)).Select(x => x.ServiceId).ToArray();
        return services.Where(x => !excludeServices.Contains(x.Id)).ToList();
    }
}
