using Csis.Admission.Application.Features.ImamJamaat.Dtos;
using Csis.Authorization.Services;
using Csis.Paging;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;
public sealed record GetMosqueListQuery : BaseSearchQuery, IRequest<IPagedList<MosqueListDto>>;

public sealed class GetMosqueListQueryHandler : IRequestHandler<GetMosqueListQuery, IPagedList<MosqueListDto>>
{
    private readonly IRepository<Domain.Entities.ImamJamaat> _repository;

    public GetMosqueListQueryHandler(IRepository<Domain.Entities.ImamJamaat> repository) {
        _repository = repository;
    }
    public async Task<IPagedList<MosqueListDto>> Handle(GetMosqueListQuery request, CancellationToken cancellationToken) {
        return await _repository
            .SearchPagedAsync<MosqueListDto>(
                request.SearchFilters,
                x => !x.Mosque.Deleted,
                request.PageIndex,
                request.PageSize,
                includeDeleted: false,
                sortExpression: request.SortBy,
                cancellationToken: cancellationToken
            );
    }
}
