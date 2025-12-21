using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

//SetStudentBlockedRepoCommand
/// <summary>تنظیم وضعیت مسدودی طلبه</summary>
public class SetStudentBlockedRepoCommand : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>توضیحات رفع مسدودی</summary>
    public string BlockReasons { get; set; }
}
