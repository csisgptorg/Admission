using Csis.Admission.Application.Features.Teaches.Dtos;

namespace Csis.Admission.Application.Features.Teaches.Queries;

/// <summary>
/// GetTeachesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetTeachesByCodmQuery(int Codm) : IRequest<List<TeachDto>>;

internal sealed class GetTeachesByCodmQueryHandler : IRequestHandler<GetTeachesByCodmQuery, List<TeachDto>>
{
    private readonly IRepository<Teach> _teachRepo;
    public GetTeachesByCodmQueryHandler(IRepository<Teach> teachRepo) {
        _teachRepo = teachRepo;
    }

    public async Task<List<TeachDto>> Handle(GetTeachesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _teachRepo.GetAllAsync<TeachDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return [.. result.OrderByDescending(x => x.Id)];
    }
}
