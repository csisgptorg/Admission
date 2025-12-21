using Csis.Admission.Application.Features.BlockServices.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Queries;

/// <summary>لیست</summary>
public sealed record StudentBlockedServicesByCodmQuery(int Codm) : IRequest<List<StudentBlockServiceDto>>;

internal sealed class StudentBlockedServicesByCodmQueryHandler(IRepository<StudentBlockService> repo)
    : IRequestHandler<StudentBlockedServicesByCodmQuery, List<StudentBlockServiceDto>>
{
    public async Task<List<StudentBlockServiceDto>> Handle(StudentBlockedServicesByCodmQuery query, CancellationToken cancellation) {
        var result = await repo.GetAllAsync<StudentBlockServiceDto>(x=>x.Codm==query.Codm,false,cancellation);
        return result;
    }
}
