using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <summary>بروز رسانی تلفن ثابت طلبه</summary>
public class UpdateStudentTelephoneRepoCommand : RepoCommandLogParam
{
    /// <inheritdoc/>
    public UpdateStudentTelephoneRepoCommand(int codm, string preCodeTel, string tel) {
        Codm = codm;
        PreCodeTel = preCodeTel;
        Tel = tel;
    }
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>پیش شماره تلفن ثابت</summary>
    public string PreCodeTel { get; set; }
    /// <summary>تلفن ثابت</summary>
    public string Tel { get; set; }
};
