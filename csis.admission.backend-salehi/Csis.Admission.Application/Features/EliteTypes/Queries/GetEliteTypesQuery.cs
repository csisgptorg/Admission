using Csis.Admission.Application.Features.EliteTypes.Dtos;

namespace Csis.Admission.Application.Features.EliteTypes.Queries;

/// <summary>دریافت لیست انواع نخبگانی</summary>
public sealed record GetEliteTypesQuery : IRequest<EliteTypeDto[]>;

internal sealed class GetEliteTypesQueryHandler : IRequestHandler<GetEliteTypesQuery, EliteTypeDto[]>
{
    private readonly IRepository<EliteType, short> _repo;
    public GetEliteTypesQueryHandler(IRepository<EliteType, short> repo) {
        _repo = repo;
    }

    public async Task<EliteTypeDto[]> Handle(GetEliteTypesQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<EliteTypeDto>(cancellationToken: cancellationToken);
        return [.. result];
    }
}


