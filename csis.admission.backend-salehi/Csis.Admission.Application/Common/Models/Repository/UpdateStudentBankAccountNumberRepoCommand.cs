using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>ثبت حساب بانکی</summary>
public class UpdateStudentBankAccountNumberRepoCommand : RepoCommandLogParam
{
    /// <inheritdoc/>
    public UpdateStudentBankAccountNumberRepoCommand(int codm, string bankAccountNumber) {
        Codm = codm;
        BankAccountNumber = bankAccountNumber;
    }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; }

    /// <summary>شماره حساب بانکی</summary>
    public string BankAccountNumber { get; }
};
