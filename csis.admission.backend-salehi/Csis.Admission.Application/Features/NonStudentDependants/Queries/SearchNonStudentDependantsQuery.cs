using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.NonStudentDependants.Dtos;
using Csis.Paging;
using Csis.Utilities.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.NonStudentDependants.Queries;

/// <summary>
/// جستجوی موجودیت تکفل های غیرطلبه ها
/// </summary>
public sealed record SearchNonStudentDependantsQuery : BaseSearchQuery, IRequest<IPagedList<NonStudentDependantDto>>;

internal sealed class SearchNonStudentDependantsQueryHandler : IRequestHandler<SearchNonStudentDependantsQuery, IPagedList<NonStudentDependantDto>>
{
    private readonly INonStudentDependantRepository _nonStudentDependantRepo;
    private readonly ILogger<SearchNonStudentDependantsQueryHandler> _logger;

    public SearchNonStudentDependantsQueryHandler(INonStudentDependantRepository nonStudentDependantRepo, ILogger<SearchNonStudentDependantsQueryHandler> logger) {
        _nonStudentDependantRepo = nonStudentDependantRepo;
        _logger = logger;
    }

    public async Task<IPagedList<NonStudentDependantDto>> Handle(SearchNonStudentDependantsQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Searching nonStudentDependants with query {query}", request.ToJson());

        var result = await _nonStudentDependantRepo.SearchPagedAsync<NonStudentDependantDto>(
            request.SearchFilters,
            null,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);

        _logger.LogDebug("Found {totalCount} nonStudentDependants in {pageCount} pages", result.TotalCount, result.TotalPages);

        return result;
    }
}
