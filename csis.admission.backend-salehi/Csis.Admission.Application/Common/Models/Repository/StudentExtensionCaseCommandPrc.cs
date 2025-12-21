using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
/// خودکار تمدید پرونده ای
/// <summary></summary>
public class StudentExtensionCaseCommandPrc : RepoCommandLogParam
{
    /// <inheritdoc/>
    public int Codm { get; set; }
};
/// <inheritdoc/>
/// دستی تمدید پرونده ای
/// <summary></summary>
public class ManualStudentExtensionCaseCommandPrc : RepoCommandLogParam
{
    /// <inheritdoc/>
    public int Codm { get; set; }
    /// <inheritdoc/>
    public string CaseValidityReasonList { get; set; }
    /// <inheritdoc/>
    public int CaseValidityDate { get; set; }
}

//StudentNormalExtensionCaseCommandPrc
/// <inheritdoc/>
/// تمدید عادی پرونده ای
/// <summary></summary>
public class StudentNormalEditCaseCommandPrc : RepoCommandLogParam
{
    /// <inheritdoc/>
    public int Codm { get; set; }
    /// <inheritdoc/>
    public string CaseDescription { get; set; }
}
