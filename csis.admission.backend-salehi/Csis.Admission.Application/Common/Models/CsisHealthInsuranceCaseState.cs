namespace Csis.Admission.Application.Common.Models;
/// <inheritdoc/>
public record CsisHealthInsuranceCaseStateResponse(string Codm, int? TakafolId, HealthInsuranceCaseStatus Status, string CaseNumber);

/// <summary>
/// وضعیت پرونده بیمه سلامت
/// </summary>
public enum HealthInsuranceCaseStatus
{
    /// <summary>
    /// فاقد پرونده
    /// </summary>
    NoInsuranceCase = 1,

    /// <summary>
    /// فعال
    /// </summary>
    Active = 2,

    /// <summary>
    /// ابطال شده
    /// </summary>
    Revoked = 3
}
