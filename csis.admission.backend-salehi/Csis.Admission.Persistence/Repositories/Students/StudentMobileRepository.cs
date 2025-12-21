using AutoMapper;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.StudentMobiles.Dtos;

namespace Csis.Admission.Persistence.Repositories.Students;
internal sealed class StudentMobileRepository : IStudentMobileRepository
{
    private readonly AppDapperContext _dapper;
    public StudentMobileRepository(IMapper mapper, AppDapperContext dapper) {
        _dapper = dapper;
    }

    public async Task<FamilyMobileDto[]> GetFamily(int codm) {
        var result = await _dapper.ExecuteProcedureToList<FamilyMobileDto>(ProcedureName.GetFamilyMobileV4, new { codm });
        return result.ToArray();
    }
    public async Task<ProcedureResultDto> Update(UpdateStudentPhoneRepoCommand command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetPhone, command);
        return result;
    }

    public async Task<ProcedureResultDto> Update(UpdateStudentMobileRepoCommand command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentMobileV4, command);
        return result;
    }

    public async Task<ProcedureResultDto> Update(UpdateStudentTelephoneRepoCommand command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentTel, command);
        return result;
    }

    public async Task<ProcedureResultDto> UpdateDependent(UpdateDependentMobileRepoCommand command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentMobileV4, command);
        return result;
    }
}
