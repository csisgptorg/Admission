using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// مدل ثبت ازدواج طالب خواهر
/// </summary>
public class SisterStudentMarriagePrcRequest : RepoCommandLogParam
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public int MarriageDate { get; set; }
}
