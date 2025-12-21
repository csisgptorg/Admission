using Csis.Admission.Application.Features.AdmissionAuditLogs.Dtos;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// IAdmissionAuditLogRepository
/// </summary>
public interface IAdmissionAuditLogRepository
{
    /// <summary>سوابق پذیرشی طلبه</summary>
    Task<StudentAdmissionAuditLogDto[]> GetStudentLogsByCodm(int codm);
    /// <summary>سوابق پذیرشی تکفل</summary>
    Task<DependentAdmissionAuditLogDto[]> GetDependentLogsByCodm(int codm);
}
