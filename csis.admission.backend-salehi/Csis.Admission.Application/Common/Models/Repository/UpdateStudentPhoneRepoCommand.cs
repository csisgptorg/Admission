using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <summary>بروز رسانی تلفن طلبه</summary>
public class UpdateStudentPhoneRepoCommand : RepoCommandLogParam
{
    /// <inheritdoc/>
    public UpdateStudentPhoneRepoCommand(int codm, string mobile, string preCodeTel, string tel) {
        Codm = codm;
        Mobile = mobile;
        PreCodeTel = preCodeTel;
        Tel = tel;
    }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>موبایل</summary>
    public string Mobile { get; set; }

    /// <summary>پیش شماره تلفن ثابت</summary>
    public string PreCodeTel { get; set; }
    /// <summary>تلفن ثابت</summary>
    public string Tel { get; set; }
};
