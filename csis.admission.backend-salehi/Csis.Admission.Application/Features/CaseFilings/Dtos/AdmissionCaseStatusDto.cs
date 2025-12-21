namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

/// <summary>
/// 
/// </summary>
public sealed record AdmissionCaseStatusDto
{
    /// <summary>
    /// دریافت توکن و موبایل
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="mobile"></param>
    public AdmissionCaseStatusDto(string identity, string mobile) {
        Identity = identity;
        Mobile = mobile;
    }

    /// <summary>
    /// توکن  
    /// </summary>
    public string Identity { get; init; }

    /// <summary>
    /// موبایل
    /// </summary>
    public string Mobile { get; init; }
}

/// <summary>
/// نتیجه نهایی پرونده
/// </summary>
/// <param name="Codm"></param>
/// <param name="Message"></param>
public sealed record AdmissionCaseStatusResultDto(int? Codm, string Message);
