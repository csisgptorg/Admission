using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Paging;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>جستجوی پیشرفته طلبه</summary>
public sealed record StudentAdvancedSearchQuery : BaseSearchQuery,IRequest<IPagedList<StudentAdvancedSearchDto>>;

internal sealed class StudentAdvancedSearchQueryHandler : IRequestHandler<StudentAdvancedSearchQuery, IPagedList<StudentAdvancedSearchDto>>
{
    private readonly IRepository<StudentSummary> _repo;
    public StudentAdvancedSearchQueryHandler(IRepository<StudentSummary> repo) {
        _repo = repo;
    }

    public async Task<IPagedList<StudentAdvancedSearchDto>> Handle(StudentAdvancedSearchQuery request, CancellationToken cancellationToken) {

        return await _repo.SearchPagedAsync<StudentAdvancedSearchDto>(request.SearchFilters,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);
    }
}
