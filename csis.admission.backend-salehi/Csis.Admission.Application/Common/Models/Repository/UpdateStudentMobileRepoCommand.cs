using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <summary>بروز رسانی موبایل طلبه</summary>
public class UpdateStudentMobileRepoCommand : RepoCommandLogParam
{
    /// <inheritdoc/>
    public UpdateStudentMobileRepoCommand(int codm, string mobile) {
        Codm = codm;
        Mobile = mobile;
    }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>موبایل</summary>
    public string Mobile { get; set; }
};

/// <summary>بروز رسانی موبایل تکفل</summary>
public sealed class UpdateDependentMobileRepoCommand : RepoCommandLogParam
{
    /// <inheritdoc/>
    public UpdateDependentMobileRepoCommand(long dependentId, int codm, string mobile) {
        DependentId = dependentId;
        Codm = codm;
        Mobile = mobile;
    }


    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>موبایل</summary>
    public string Mobile { get; set; }
};
