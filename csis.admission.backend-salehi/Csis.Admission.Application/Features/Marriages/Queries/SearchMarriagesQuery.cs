using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.Marriages.Dtos;
using Csis.Paging;

namespace Csis.Admission.Application.Features.Marriages.Queries;

/// <summary>
/// جستجوی موجودیت ازدواج ها
/// </summary>
public sealed record SearchMarriagesQuery : BaseSearchQuery, IRequest<IPagedList<MarriageDto>>;

internal sealed class SearchMarriagesQueryHandler : IRequestHandler<SearchMarriagesQuery, IPagedList<MarriageDto>>
{
    private readonly IPersonMarriageRepository _marriageRepo;
    private readonly ILogger<SearchMarriagesQueryHandler> _logger;

    public SearchMarriagesQueryHandler(IPersonMarriageRepository marriageRepo, ILogger<SearchMarriagesQueryHandler> logger) {
        _marriageRepo = marriageRepo;
        _logger = logger;
    }

    public async Task<IPagedList<MarriageDto>> Handle(SearchMarriagesQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Searching marriages with query {query}", request.ToJson());

        var result = await _marriageRepo.SearchPagedAsync<MarriageDto>(
            request.SearchFilters,
            null,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);

        _logger.LogDebug("Found {totalCount} marriages in {pageCount} pages", result.TotalCount, result.TotalPages);

        return result;
    }
}
