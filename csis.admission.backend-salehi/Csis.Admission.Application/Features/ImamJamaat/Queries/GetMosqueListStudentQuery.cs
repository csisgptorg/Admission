using Csis.Admission.Application.Features.ImamJamaat.Dtos;
using Csis.Authorization.Services;
using Csis.Paging;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;

public sealed record GetMosqueListStudentQuery : BaseSearchQuery, IRequest<IPagedList<MosqueListDto>>;

public sealed class GetMosqueListStudentQueryHandler : IRequestHandler<GetMosqueListStudentQuery, IPagedList<MosqueListDto>>
{
    private readonly IRepository<Domain.Entities.ImamJamaat> _repository;
    private readonly ICsisAuthenticatedUserService _csisAuthenticatedUserService;

    public GetMosqueListStudentQueryHandler(IRepository<Domain.Entities.ImamJamaat> repository, ICsisAuthenticatedUserService csisAuthenticatedUserService) {
        _repository = repository;
        _csisAuthenticatedUserService = csisAuthenticatedUserService;
    }
    public async Task<IPagedList<MosqueListDto>> Handle(GetMosqueListStudentQuery request, CancellationToken cancellationToken) {
        var codM = int.Parse(await _csisAuthenticatedUserService.GetStudentCodmAsync());
        return await _repository
            .SearchPagedAsync<MosqueListDto>(
                request.SearchFilters,
                x => x.CodM == codM && !x.Mosque.Deleted,
                request.PageIndex,
                request.PageSize,
                sortExpression: request.SortBy,
                cancellationToken: cancellationToken
            );
    }
}
