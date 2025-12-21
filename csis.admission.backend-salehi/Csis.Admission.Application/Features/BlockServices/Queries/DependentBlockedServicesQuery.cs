using Csis.Admission.Application.Features.BlockServices.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Queries;

/// <summary>دریافت لیست خدمات مسدود تکفل ها براساس کد مرکز خدمات</summary>
public sealed record DependentBlockedServicesQuery(int Codm,long? DependentId) : IRequest<List<DependentBlockServiceDto>>;

internal sealed class DependentBlockedServicesQueryHandler(IRepository<DependentBlockService> repo) 
    : IRequestHandler<DependentBlockedServicesQuery, List<DependentBlockServiceDto>>
{
    public async Task<List<DependentBlockServiceDto>> Handle(DependentBlockedServicesQuery query, CancellationToken cancellation) {
        var result = await repo.GetAllAsync<DependentBlockServiceDto>
            (x=>x.Codm==query.Codm && (query.DependentId==null || query.DependentId == 0 || x.DependentId==query.DependentId),false,cancellation);
        return result;
    }
}
