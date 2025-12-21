using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Features.CommissionsInfos.Dtos;

namespace Csis.Admission.Application.Features.CommissionInfos.Queries;

/// <summary>کمسیون تکفل</summary>
public sealed record GetDependentCommissionsInfoByCodmQuery(int Codm) : IRequest<DependentCommissionInfoDto[]>;

internal sealed class GetDependentCommissionsInfoByCodmQueryHandler : IRequestHandler<GetDependentCommissionsInfoByCodmQuery, DependentCommissionInfoDto[]>
{
    private readonly IStudentRepository _repo;
    public GetDependentCommissionsInfoByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<DependentCommissionInfoDto[]> Handle(GetDependentCommissionsInfoByCodmQuery request, CancellationToken cancellationToken) {

        return await _repo.GetDependentCommissionRequestByCodm(request.Codm)
            ?? throw new RecordNotFoundException<DependentCommissionInfoDto>(request.Codm);
    }
}
