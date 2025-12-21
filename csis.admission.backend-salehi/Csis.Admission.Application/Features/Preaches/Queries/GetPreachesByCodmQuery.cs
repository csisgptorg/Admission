using Csis.Admission.Application.Features.Preaches.Dtos;

namespace Csis.Admission.Application.Features.Preaches.Queries;

/// <summary>دریافت لیست تبلیغ با کد مرکز خدمات</summary>
public sealed record GetPreachesByCodmQuery(int Codm) : IRequest<List<PreachDto>>;

internal sealed class GetPreachesByCodmQueryHandler(IRepository<Preach> repo) : IRequestHandler<GetPreachesByCodmQuery, List<PreachDto>>
{
    public async Task<List<PreachDto>> Handle(GetPreachesByCodmQuery request, CancellationToken cancellationToken) {
        var result = (await repo.GetAllAsync<PreachDto>(x=>x.Codm==request.Codm,cancellationToken: cancellationToken)).OrderByDescending(x => x.Id);
        return result.ToList();
    }
}
