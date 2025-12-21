using Microsoft.AspNetCore.Http;

namespace Csis.Admission.Application.Extensions;

/// <summary>IHttpContextAccessorExtension</summary>
public static partial class IHttpContextAccessorExtensions
{
    /// <summary>دریافت آی پی کاربر</summary>
    public static string GetClientIP(this IHttpContextAccessor httpContextAccessor) {
        var request = httpContextAccessor?.HttpContext?.Request;

        string headerIp = request?.Headers?["X-FORWARDED-FOR"].FirstOrDefault() ?? "";
        headerIp = headerIp == "" ? request?.Headers?["HTTP_X_FORWARDED_FOR"].FirstOrDefault() ?? "" : headerIp;
        headerIp = headerIp == "" ? request?.Headers?["REMOTE_ADDR"].FirstOrDefault() ?? "" : headerIp;
        var remoteIpAddress = request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
        headerIp = string.IsNullOrEmpty(headerIp) ? remoteIpAddress : headerIp;

        if ( string.IsNullOrEmpty(headerIp) || !System.Net.IPAddress.TryParse(headerIp, out System.Net.IPAddress ip) ) {
            headerIp = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";
        }

        return headerIp;
    }
}
