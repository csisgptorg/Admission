namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

/// <summary>
/// مدل خروجی کپچا
/// </summary>
public sealed class CaptchaDto
{
    /// <summary>
    /// توکن
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// عکس
    /// </summary>
    public string ImageBase64 { get; set; }
}
