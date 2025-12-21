using Csis.Authorization.Models;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Auth.Dtos;

/// <summary>
/// نتیجه ورود به سامانه
/// </summary>
public sealed class LoginResultDto
{
    /// <summary>
    /// اطلاعات توکن احراز هویت
    /// </summary>
    public TokenResponse TokenInfo { get; set; }

    /// <summary>
    /// موفقیت عملیات ورود
    /// </summary>
    [JsonIgnore]
    public bool Succeeded { get; set; }

    /// <summary>
    /// پیام خطا
    /// </summary>
    [JsonIgnore]
    public string ErrorMessage { get; set; }
}
