namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// پاسخ دریافت تصویر ایرانی از ثبت احوال
/// </summary>
public sealed class GetIranianImageFromSabteAhvalResponse
{
    /// <summary>تصاویر</summary>
    public List<GetIdentityInfoByNationalCodeResponseImage> Images { get; set; }
}
