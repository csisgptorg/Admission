using AutoMapper;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.AdmissionAuditLogs.Dtos;

namespace Csis.Admission.Persistence.Repositories;
internal sealed class AdmissionAuditLogRepository : IAdmissionAuditLogRepository
{
    private readonly IMapper _mapper;
    private readonly AppDapperContext _dapper;
    public AdmissionAuditLogRepository(IMapper mapper, AppDapperContext dapper) {
        _mapper = mapper;
        _dapper = dapper;
    }

    /// <summary>سوابق پذیرشی طلبه</summary>
    public async Task<StudentAdmissionAuditLogDto[]> GetStudentLogsByCodm(int codm) {
        var commissions = await _dapper.ExecuteProcedureToList<StudentAdmissionAuditLog>(ProcedureName.GetStudentAuditLog, new { codm});
        var result = commissions.Select(_mapper.Map<StudentAdmissionAuditLogDto>).ToArray();
        return result;
    }

    /// <summary>سوابق پذیرشی تکفل</summary>
    public async Task<DependentAdmissionAuditLogDto[]> GetDependentLogsByCodm(int codm) {
        var commissions = await _dapper.ExecuteProcedureToList<DependentAdmissionAuditLog>(ProcedureName.GetDependentAuditLog, new { codm });
        var result = commissions.Select(_mapper.Map<DependentAdmissionAuditLogDto>).ToArray();
        return result;
    }
}
