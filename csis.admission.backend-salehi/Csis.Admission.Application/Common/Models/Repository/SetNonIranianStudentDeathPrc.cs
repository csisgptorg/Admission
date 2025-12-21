using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>تنظیم وضعیت فوت طلبه غیرایرانی</summary>
public class SetNonIranianStudentDeathPrc : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>تاریخ فوت</summary>
    public int DeathDate { get; set; }
}
