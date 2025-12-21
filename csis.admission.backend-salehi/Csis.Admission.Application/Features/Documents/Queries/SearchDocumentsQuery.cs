using Csis.Paging;
using Csis.Admission.Application.Features.Documents.Dtos;

namespace Csis.Admission.Application.Features.Documents.Queries;

/// <inheritdoc/>
public sealed record SearchDocumentsQuery : BaseSearchQuery, IRequest<IPagedList<RequestDocumentDto>>;

internal sealed class SearchDocumentsQueryHandler : IRequestHandler<SearchDocumentsQuery, IPagedList<RequestDocumentDto>>
{
    private readonly IRepository<RequestDocument,long> _repo;
    public SearchDocumentsQueryHandler(IRepository<RequestDocument, long> repo) {
        _repo= repo;
    }

    public async Task<IPagedList<RequestDocumentDto>> Handle(SearchDocumentsQuery request, CancellationToken cancellationToken) {

        var result = await _repo.SearchPagedAsync<RequestDocumentDto>(
            request.SearchFilters,
            null,
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            cancellationToken: cancellationToken);

        return result;
    }
}
