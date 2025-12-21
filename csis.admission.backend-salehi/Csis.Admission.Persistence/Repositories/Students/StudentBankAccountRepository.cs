using AutoMapper;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.BankAccounts.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Persistence.Repositories.Students;

internal sealed class StudentBankAccountRepository : IStudentBankAccountRepository
{
    private readonly AppDapperContext _dapper;
    public StudentBankAccountRepository(IMapper mapper, AppDapperContext dapper) {
        _dapper = dapper;
    }

    public async Task<bool> CheckDuplicateBankAccount(int codm, long? dependentId, string bankAccountNumber) {
        var result = await _dapper.ExecuteProcedureToList<FamilyBankAccountDto>(ProcedureName.CheckDuplicateBankAccountNumberV4, new { codm, dependentId, bankAccountNumber });
        return result.Count > 0;
    }

    public async Task<FamilyBankAccountDto[]> GetFamiliesByCodm(int codm) {
        var result = await _dapper.ExecuteProcedureToList<FamilyBankAccountDto>(ProcedureName.GetFamilyBankAccountNumberV4, new { codm });
        return [.. result];
    }

    public async Task<FamilyKhadamatCardDto[]> GetFamiliesKhadamatCardsByCodm(int codm) {
        var result = await _dapper.ExecuteProcedureToList<FamilyKhadamatCardDto>(ProcedureName.GetFamilyKhadamatCardV4, new { codm });
        return [.. result];
    }

    public async Task<ProcedureResultDto> Update(UpdateStudentBankAccountNumberRepoCommand command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetStudentBankAccountNumberV4, command);
        return result;
    }

    public async Task<ProcedureResultDto> UpdateDependent(UpdateDependentBankAccountNumberRepoCommand command) {
        var result = await _dapper.ExecuteProcedureSingleOrDefault<ProcedureResultDto>(ProcedureName.SetDependentBankAccountNumberV4, command);
        return result;
    }
}
