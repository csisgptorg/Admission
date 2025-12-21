using Csis.Admission.Application.Features.Branches.Dtos;

namespace Csis.Admission.Application.Features.Branches.Queries;

/// <summary>دریافت لیست شعب</summary>
public sealed record ValidateBranchesByProviceIdQuery(short BranchId, short ProvinceId) : IRequest<bool>;

internal sealed class ValidateBranchesByProviceIdQueryHandler(IRepository<Branch, short> repo) : IRequestHandler<ValidateBranchesByProviceIdQuery, bool>
{
    public async Task<bool> Handle(ValidateBranchesByProviceIdQuery request, CancellationToken cancellationToken) {
        var a = await repo.GetAllAsync<BranchDto>(cancellationToken: cancellationToken);
        return await repo.ExistsAsync(x => x.Id == request.BranchId && x.ProvinceId == request.ProvinceId, cancellationToken: cancellationToken);
    }
}
