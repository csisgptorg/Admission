using Csis.Admission.Application.Features.Memorizers.Dtos;

namespace Csis.Admission.Application.Features.Memorizers.Queries;

/// <summary>حافظین</summary>
public sealed record GetStudentMemorizerByCodmQuery(int Codm) : IRequest<List<StudentMemorizerDto>>;

internal sealed class GetStudentMemorizerByCodmQueryHandler : IRequestHandler<GetStudentMemorizerByCodmQuery, List<StudentMemorizerDto>>
{
    private readonly IRepository<Memorizer> _repo;
    public GetStudentMemorizerByCodmQueryHandler(IRepository<Memorizer> repo) {
        _repo = repo;
    }

    public async Task<List<StudentMemorizerDto>> Handle(GetStudentMemorizerByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<StudentMemorizerDto>(x=>x.Codm==request.Codm, false,cancellationToken);
        return [..result.OrderByDescending(x=> x.Id)];
    }
}
