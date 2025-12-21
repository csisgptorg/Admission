using Csis.Admission.Application.Features.Branches.Dtos;

namespace Csis.Admission.Application.Features.Branches.Queries;

/// <summary>دریافت لیست استان ها</summary>
public sealed record GetAgenciesByBranchIdQuery(short BranchId) : IRequest<AgencyDto[]>;

internal sealed class GetAgenciesByBranchIdQueryHandler : IRequestHandler<GetAgenciesByBranchIdQuery, AgencyDto[]>
{
    private readonly IRepository<Agency, short> _repo;
    public GetAgenciesByBranchIdQueryHandler(IRepository<Agency, short> repo) {
        _repo = repo;
    }

    public async Task<AgencyDto[]> Handle(GetAgenciesByBranchIdQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<AgencyDto>(x=>x.BranchId==request.BranchId);
        return result.ToArray();
    }
}
