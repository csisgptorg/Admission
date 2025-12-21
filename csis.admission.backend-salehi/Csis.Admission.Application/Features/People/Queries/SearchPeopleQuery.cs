using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.People.Dtos;
using Csis.Paging;

namespace Csis.Admission.Application.Features.People.Queries;

/// <summary>
/// جستجوی موجودیت شخص ها
/// </summary>
public sealed record SearchPeopleQuery : BaseSearchQuery, IRequest<IPagedList<PersonDto>>;

internal sealed class SearchPeopleQueryHandler : IRequestHandler<SearchPeopleQuery, IPagedList<PersonDto>>
{
    private readonly IPersonRepository _personRepo;
    private readonly ILogger<SearchPeopleQueryHandler> _logger;

    public SearchPeopleQueryHandler(IPersonRepository personRepo, ILogger<SearchPeopleQueryHandler> logger) {
        _personRepo = personRepo;
        _logger = logger;
    }

    public async Task<IPagedList<PersonDto>> Handle(SearchPeopleQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Searching people with query {query}", request.ToJson());

        var result = await _personRepo.SearchPagedAsync<PersonDto>(
            request.SearchFilters,
            null,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);

        _logger.LogDebug("Found {totalCount} people in {pageCount} pages", result.TotalCount, result.TotalPages);

        return result;
    }
}
