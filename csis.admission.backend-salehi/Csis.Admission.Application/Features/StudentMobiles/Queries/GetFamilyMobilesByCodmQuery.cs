using Csis.Admission.Application.Features.StudentMobiles.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.StudentMobiles.Queries;

/// <inheritdoc/>
public sealed record GetFamilyMobilesByCodmQuery(int Codm) : IRequest<FamilyMobileDto[]>;

internal sealed class GetStudentFamilyMobilesByCodmQueryHandler(
    IStudentMobileRepository repo) : IRequestHandler<GetFamilyMobilesByCodmQuery, FamilyMobileDto[]>
{
    public async Task<FamilyMobileDto[]> Handle(GetFamilyMobilesByCodmQuery request, CancellationToken cancellationToken) {
        return await repo.GetFamily(request.Codm);
    }
}
