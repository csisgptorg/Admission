namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// مدل پاسخ اعتبارسنجی مالکیت شبا
/// </summary>
public class ValidateShebaOwnershipResponse
{
    public string Message { get; set; }
    public bool? IsMatched { get; set; }
    public string ShebaNumber { get; set; }
}
