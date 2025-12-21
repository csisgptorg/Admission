using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// ایجاد تغییر پرونده دانشجو
/// </summary>
public class CreateStudentCaseDescriptionPrc : RepoCommandLogParam
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>شرح پرونده</summary>
    public string CaseDescription { get; set; }
}
