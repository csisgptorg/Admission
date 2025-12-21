using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// دریافت کمیسیون بازنشستگی افراد تحت تکفل
/// </summary>
public class GetDependentPensionCommissionModel
{

    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// شناسه فرد تحت تکفل
    /// </summary>
    public int ExpireDate { get; set; }
}
