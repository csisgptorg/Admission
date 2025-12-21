using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// دریافت کمیسیون تکفل
/// </summary>
public class GetDependentCommissionPrc : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
}
