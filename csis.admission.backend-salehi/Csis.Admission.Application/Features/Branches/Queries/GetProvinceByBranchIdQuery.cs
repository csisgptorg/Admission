using Csis.Admission.Application.Features.Branches.Dtos;

namespace Csis.Admission.Application.Features.Branches.Queries;

/// <summary>دریافت استان مربوط به شعبه</summary>
public sealed record GetProvinceByBranchIdQuery(short BranchId) : IRequest<BranchDto>;

internal sealed class GetProvinceByBranchIdQueryHandler : IRequestHandler<GetProvinceByBranchIdQuery, BranchDto>
{
    private readonly IRepository<Branch, short> _repo;
    public GetProvinceByBranchIdQueryHandler(IRepository<Branch, short> repo) {
        _repo = repo;
    }

    public async Task<BranchDto> Handle(GetProvinceByBranchIdQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetOneAsync<BranchDto>(x => x.Id == request.BranchId);
        return result;
    }
}
