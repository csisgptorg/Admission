using Csis.Admission.Application.Features.Educations.Dtos;

namespace Csis.Admission.Application.Features.Educations.Queries;

/// <inheritdoc/>
public sealed record GetEducationByCodmQuery(int Codm) : IRequest<EducationDto>;

internal sealed class GetEducationByCodmQueryHandler : IRequestHandler<GetEducationByCodmQuery, EducationDto>
{
    private readonly IRepository<Education> _repo;
    public GetEducationByCodmQueryHandler(IRepository<Education> repo) {
        _repo = repo;
    }

    public async Task<EducationDto> Handle(GetEducationByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetOneAsync<EducationDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return result;
    }
}
