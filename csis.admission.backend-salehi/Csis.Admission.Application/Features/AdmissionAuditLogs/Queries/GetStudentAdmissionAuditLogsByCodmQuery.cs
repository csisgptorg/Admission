using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.AdmissionAuditLogs.Dtos;

namespace Csis.Admission.Application.Features.AdmissionAuditLogs.Queries;

/// <summary>سوابق پذیرشی طلبه</summary>
public sealed record GetStudentAdmissionAuditLogsByCodmQuery(int Codm) : IRequest<StudentAdmissionAuditLogDto[]>;

internal sealed class GetStudentAdmissionAuditLogsByCodmQueryHandler : IRequestHandler<GetStudentAdmissionAuditLogsByCodmQuery, StudentAdmissionAuditLogDto[]>
{
    private readonly IAdmissionAuditLogRepository _repo;
    public GetStudentAdmissionAuditLogsByCodmQueryHandler(IAdmissionAuditLogRepository repo) {
        _repo = repo;
    }

    public async Task<StudentAdmissionAuditLogDto[]> Handle(GetStudentAdmissionAuditLogsByCodmQuery request, CancellationToken cancellationToken) {

        var result = await _repo.GetStudentLogsByCodm(request.Codm)
            ?? throw new RecordNotFoundException<StudentAdmissionAuditLogDto>(request.Codm);

        return [.. result.OrderByDescending(x => x.Id)];
    }
}
