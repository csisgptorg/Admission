using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.NonStudents.Dtos;
using Csis.Paging;
using Csis.Utilities.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Application.Features.NonStudents.Queries;

/// <summary>
/// جستجوی موجودیت غیر طلبه ها
/// </summary>
public sealed record SearchNonStudentsQuery : BaseSearchQuery, IRequest<IPagedList<NonStudentDto>>;

internal sealed class SearchNonStudentsQueryHandler : IRequestHandler<SearchNonStudentsQuery, IPagedList<NonStudentDto>>
{
    private readonly INonStudentRepository _nonStudentRepo;
    private readonly ILogger<SearchNonStudentsQueryHandler> _logger;

    public SearchNonStudentsQueryHandler(INonStudentRepository nonStudentRepo, ILogger<SearchNonStudentsQueryHandler> logger) {
        _nonStudentRepo = nonStudentRepo;
        _logger = logger;
    }

    public async Task<IPagedList<NonStudentDto>> Handle(SearchNonStudentsQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Searching nonStudents with query {query}", request.ToJson());

        var result = await _nonStudentRepo.SearchPagedAsync<NonStudentDto>(
            request.SearchFilters,
            null,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);

        _logger.LogDebug("Found {totalCount} nonStudents in {pageCount} pages", result.TotalCount, result.TotalPages);

        return result;
    }
}
