using Csis.Admission.Application.Features.Schools.Dtos;

namespace Csis.Admission.Application.Features.Schools.Queries;

/// <summary>دریافت لیست مدارس</summary>
public sealed record GetSchoolsQuery : IRequest<SchoolDto[]>;

internal sealed class GetSchoolsQueryHandler : IRequestHandler<GetSchoolsQuery, SchoolDto[]>
{
    private readonly IRepository<School, short> _repo;
    public GetSchoolsQueryHandler(IRepository<School, short> repo) {
        _repo = repo;
    }

    public async Task<SchoolDto[]> Handle(GetSchoolsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<SchoolDto>(cancellationToken:cancellationToken);
        return [.. result];
    }
}


