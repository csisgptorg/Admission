using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>بروز رسانی اطلاعات شناسنامه ای تکفل</summary>
public class UpdateDependentBirthCertInfoRepoCommand : RepoCommandLogParam
{
    /// <summary>کد تکفل</summary>
    public long Id { get; set; }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>کد ملی</summary>
    public string NationalCode { get; set; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; set; }

    /// <summary>تاریخ تولید</summary>
    public int BirthDate { get; set; }

    /// <summary>مذهب</summary>
    public Religion? Religion { get; set; }

    /// <summary>سید</summary>
    public bool? IsSadat { get; set; }

    /// <summary>توضیحات شناسنامه</summary>
    public string BirthCertDescription { get; set; }
}
