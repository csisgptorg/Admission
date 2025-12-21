using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.BankAccounts.Dtos;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>ریپو حساب بانکی</summary>
public interface IStudentBankAccountRepository
{
    /// <summary>لیست حساب بانکی اعضای خانواده</summary>
    Task<FamilyBankAccountDto[]> GetFamiliesByCodm(int codm);

    /// <summary>لیست خدمات کارت اعضای خانواده</summary>
    Task<FamilyKhadamatCardDto[]> GetFamiliesKhadamatCardsByCodm(int codm);

    /// <summary>بروز رسانی حساب بانکی طلبه</summary>
    Task<ProcedureResultDto> Update(UpdateStudentBankAccountNumberRepoCommand command);

    /// <summary>بروز رسانی حساب بانکی تکفل</summary>
    Task<ProcedureResultDto> UpdateDependent(UpdateDependentBankAccountNumberRepoCommand command);

    /// <summary>بررسی حساب بانکی تکراری</summary>
    Task<bool> CheckDuplicateBankAccount(int codm, long? dependentId, string bankAccountNumber);
}

