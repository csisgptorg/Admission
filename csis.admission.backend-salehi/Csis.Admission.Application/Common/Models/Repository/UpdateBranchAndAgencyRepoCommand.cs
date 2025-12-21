using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>بروز رسانی شعبه و نمایندگی</summary>
public class UpdateBranchAndAgencyRepoCommand : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
}
