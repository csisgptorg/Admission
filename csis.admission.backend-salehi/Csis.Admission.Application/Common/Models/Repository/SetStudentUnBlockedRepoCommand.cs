using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

//SetStudentUnBlockedRepoCommand
/// <summary>تنظیم وضعیت رفع مسدودی طلبه</summary>
public class SetStudentUnBlockedRepoCommand : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
}
