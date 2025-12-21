using Csis.Paging;
using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>جستجوی پیشرفته طلبه</summary>
public sealed record DependentAdvancedSearchQuery : BaseSearchQuery,IRequest<IPagedList<DependentAdvancedSearchDto>>;

internal sealed class DependentAdvancedSearchQueryHandler : IRequestHandler<DependentAdvancedSearchQuery, IPagedList<DependentAdvancedSearchDto>>
{
    private readonly IRepository<DependentSummary,long> _repo;
    public DependentAdvancedSearchQueryHandler(IRepository<DependentSummary, long> repo) {
        _repo = repo;
    }

    public async Task<IPagedList<DependentAdvancedSearchDto>> Handle(DependentAdvancedSearchQuery request, CancellationToken cancellationToken) {

        return await _repo.SearchPagedAsync<DependentAdvancedSearchDto>(request.SearchFilters,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);
    }
}
