using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;

namespace Csis.Admission.Persistence.Repositories.Students;
internal sealed class StudentDependentRepository : IStudentDependentRepository
{
    private readonly AppDapperContext _dapper;

    public StudentDependentRepository(AppDapperContext dapper) {
        _dapper = dapper;
    }

    /// <inheritdoc/>
    public async Task<ProcedureResultDto> Create(StudentDependentRegistryPrcRequest command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetNewDependent, command);
        return result;
    }

    public async Task<ProcedureResultDto> CreateSisterStudentMarriageAsync(SisterStudentMarriagePrcRequest command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentSisterMarriage, command); 
        return result;
    }

    /// <returns></returns>
    public async Task<ProcedureResultDto> UpdateDependentChildMarriageAsync(UpdateDependentMarriagePrcRequest command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentChildMarriage, command);
        return result;
    }


    /// <returns></returns>
    public async Task<ProcedureResultDto> UpdateDependentSpouseMarriageAsync(UpdateDependentMarriagePrcRequest command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentSpouseMarriage, command);
        return result;
    }

    public async Task<ProcedureResultDto> UpdateDependentSpouseDivorceAsync(SetDependentDivorceModel command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentSpouseDivorce, command);
        return result;
    }

    public async Task<ProcedureResultDto> UpdateDependentChildDivorceAsync(SetDependentDivorceModel command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentChildDivorce, command);
        return result;
    }

    public async Task<ProcedureResultDto> DeActiveCaseDependent(DeActiveDependentV4Model command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.DeActiveDependentV4, command);
        return result;
    }

    public Task<GetDependentPensionCommissionModel> GetDependentPensionCommission(int codm, long dependentId) {
       var result = _dapper.ExecuteProcedureSingleOrDefault<GetDependentPensionCommissionModel>(ProcedureName.GetDependentPensionCommission, new { Codm = codm, DependentId = dependentId });
       return result;   
    }
}
