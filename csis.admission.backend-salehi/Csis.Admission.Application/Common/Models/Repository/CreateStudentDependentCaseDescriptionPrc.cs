using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// ایجاد تغییر پرونده تکفل دانشجو
/// </summary>
public class CreateStudentDependentCaseDescriptionPrc : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }
    /// <summary>شرح پرونده</summary>
    public string CaseDescription { get; set; } = string.Empty;
}
