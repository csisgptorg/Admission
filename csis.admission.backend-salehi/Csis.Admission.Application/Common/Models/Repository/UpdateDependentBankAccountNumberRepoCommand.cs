using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Common.Models;

/// <summary>ثبت حساب بانکی</summary>
public sealed class UpdateDependentBankAccountNumberRepoCommand : UpdateStudentBankAccountNumberRepoCommand
{
    /// <inheritdoc/>
    public UpdateDependentBankAccountNumberRepoCommand(long dependentId, int codm, string bankAccountNumber) : base(codm, bankAccountNumber) {
        DependentId = dependentId;
    }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }
};
