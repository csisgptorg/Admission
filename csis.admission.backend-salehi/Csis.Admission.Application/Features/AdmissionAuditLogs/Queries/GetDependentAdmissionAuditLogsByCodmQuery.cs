using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.AdmissionAuditLogs.Dtos;

namespace Csis.Admission.Application.Features.AdmissionAuditLogs.Queries;

/// <summary>سوابق پذیرشی تکفل</summary>
public sealed record GetDependentAdmissionAuditLogsByCodmQuery(int Codm) : IRequest<DependentAdmissionAuditLogDto[]>;

internal sealed class GetDependentAdmissionAuditLogsByCodmQueryHandler : IRequestHandler<GetDependentAdmissionAuditLogsByCodmQuery, DependentAdmissionAuditLogDto[]>
{
    private readonly IAdmissionAuditLogRepository _repo;
    public GetDependentAdmissionAuditLogsByCodmQueryHandler(IAdmissionAuditLogRepository repo) {
        _repo = repo;
    }

    public async Task<DependentAdmissionAuditLogDto[]> Handle(GetDependentAdmissionAuditLogsByCodmQuery request, CancellationToken cancellationToken) {

        var result = await _repo.GetDependentLogsByCodm(request.Codm)
            ?? throw new RecordNotFoundException<DependentAdmissionAuditLogDto>(request.Codm);

        return [.. result.OrderByDescending(x => x.Id)];
    }
}
