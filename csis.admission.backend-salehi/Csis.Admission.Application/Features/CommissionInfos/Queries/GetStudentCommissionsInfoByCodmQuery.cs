using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Features.CommissionInfos.Dtos;

namespace Csis.Admission.Application.Features.CommissionInfos.Queries;

/// <summary>کمسیون طلبه</summary>
public sealed record GetStudentCommissionsInfoByCodmQuery(int Codm) : IRequest<StudentCommissionInfoDto[]>;

internal sealed class GetStudentCommissionsInfoByCodmQueryHandler : IRequestHandler<GetStudentCommissionsInfoByCodmQuery, StudentCommissionInfoDto[]>
{
    private readonly IStudentRepository _repo;
    public GetStudentCommissionsInfoByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<StudentCommissionInfoDto[]> Handle(GetStudentCommissionsInfoByCodmQuery request, CancellationToken cancellationToken) {

        return await _repo.GetStudentCommissionRequestByCodm(request.Codm)
            ?? throw new RecordNotFoundException<StudentCommissionInfoDto>(request.Codm);
    }
}
