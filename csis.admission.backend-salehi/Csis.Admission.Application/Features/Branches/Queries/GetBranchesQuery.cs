using Csis.Admission.Application.Features.Branches.Dtos;

namespace Csis.Admission.Application.Features.Branches.Queries;

/// <summary>دریافت لیست شعب</summary>
public sealed record GetBranchesQuery(bool? HasAgency) : IRequest<BranchDto[]>;

internal sealed class GetBranchesQueryHandler(IRepository<Branch, short> repo) : IRequestHandler<GetBranchesQuery, BranchDto[]>
{
    public async Task<BranchDto[]> Handle(GetBranchesQuery query, CancellationToken cancellationToken) {
        var result = await repo.GetAllAsync<BranchDto>(x=>!query.HasAgency.HasValue || x.HasAgency==query.HasAgency.Value);
        return result.ToArray();
    }
}
