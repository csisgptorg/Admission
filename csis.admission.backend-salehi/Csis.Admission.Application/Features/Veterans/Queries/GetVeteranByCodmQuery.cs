using Csis.Admission.Application.Features.Veterans.Dtos;

namespace Csis.Admission.Application.Features.Veterans.Queries;

/// <summary>
/// GetVeteranByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetVeteranByCodmQuery(int Codm) : IRequest<VeteranDto>;

internal sealed class GetVeteranByCodmQueryHandler(IRepository<Veteran> repo)
    : IRequestHandler<GetVeteranByCodmQuery, VeteranDto>
{
    public async Task<VeteranDto> Handle(GetVeteranByCodmQuery request, CancellationToken cancellationToken) {
        var result = await repo.GetOneAsync<VeteranDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return result;
    }
}
