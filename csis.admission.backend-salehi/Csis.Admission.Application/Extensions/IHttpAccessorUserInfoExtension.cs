using Microsoft.AspNetCore.Http;

namespace Csis.Admission.Application.Extensions;
//TODO نیازمند ریفکتورینگ است

/// نگهداری اطلاعات عامل کاربر شامل
public class UserAgentInfo
{
    /// <summary>رشته کامل User-Agent ارسال‌شده توسط مرورگر.</summary>
    public string UserAgent { get; set; }

    /// <summary>نام (و در صورت امکان نسخه) مرورگر شناسایی‌شده.</summary>
    public string Browser { get; set; }

    /// <summary>نام و نسخه سیستم‌عامل استخراج‌شده از User-Agent.</summary>
    public string Os { get; set; }

    /// <summary>آدرس IP کلاینت (برگرفته از هدرهای فوروارد یا اتصال).</summary>
    public string IP { get; set; }

    /// <summary>آدرس IP ریموت اتصال (RemoteIpAddress) سرور.</summary>
    public string RemoteIP { get; set; }

    /// <summary>آدرس کامل درخواست (Full Request URL).</summary>
    public string RequestUrl { get; set; }
}

public static partial class IHttpContextAccessorExtensions
{
    public static UserAgentInfo GetUserAgent(this IHttpContextAccessor httpContextAccessor) {
        var request = httpContextAccessor?.HttpContext?.Request;
        try {
            string headerIP = request?.Headers?["X-FORWARDED-FOR"].FirstOrDefault() ?? "";
            headerIP = headerIP == "" ? request?.Headers?["HTTP_X_FORWARDED_FOR"].FirstOrDefault() ?? "" : headerIP;
            headerIP = headerIP == "" ? request?.Headers?["REMOTE_ADDR"].FirstOrDefault() ?? "" : headerIP;
            var remoteIpAddress = request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
            remoteIpAddress = remoteIpAddress.Trim().Length > 0 ? $",remoteip:{remoteIpAddress.Trim()}" : "";
            if ( string.IsNullOrEmpty(headerIP) || !System.Net.IPAddress.TryParse(headerIP, out System.Net.IPAddress ip) ) {
                headerIP = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";
            }
            var browserInfo = request?.Headers?["User-Agent"].FirstOrDefault() ?? "";
            var userAgentInfo = new ClientUserAgentInfo(browserInfo);

            var fullUrl = request == null ? "" : $"{(request?.Scheme ?? "")}://{(request?.Host.Value ?? "")}{(request.Path.Value ?? "")}{request?.QueryString}";
            return new UserAgentInfo {
                UserAgent = request != null ? browserInfo : "request is null",
                Browser = userAgentInfo.getBrowserName(browserInfo),
                Os = $"{userAgentInfo.os_name}-{userAgentInfo.os_version}",
                IP = headerIP,// httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString()
                RemoteIP = remoteIpAddress,
                RequestUrl = fullUrl ?? "",
            };
        } catch ( Exception ex ) {
            return new UserAgentInfo {
                UserAgent = ex.Message,
                Browser = request != null ? "request is ok " : "request is null",
                Os = ex.Message,
                IP = "",
                RequestUrl = ""
            };
        }
    }
}

/// <summary>کلاس کمکی برای استخراج اطلاعات سیستم‌عامل و مرورگر از رشته User-Agent.</summary>
public class ClientUserAgentInfo
{
    /// <summary>نام سیستم عامل</summary>
    public string os_name { get; set; }

    /// <summary>ورژن سیستم عامل</summary>
    public string os_version { get; set; }

    public ClientUserAgentInfo(string browserInfo = "") {

        if ( string.IsNullOrEmpty(browserInfo) ) { os_name = ""; os_version = ""; return; }

        var ua = browserInfo;
        if ( ua.Contains("Android") ) {
            os_name = "Android";
            SetVersion(ua, "Android");
            return;
        }

        if ( ua.Contains("iPhone") ) {
            os_name = "iPhone";
            SetVersion(ua, "OS");
            return;
        }

        if ( ua.Contains("iPad") ) {
            os_name = "iPad";
            SetVersion(ua, "OS");
            return;
        }

        if ( ua.Contains("Mac OS") ) {
            os_name = "Mac OS";
            return;
        }

        if ( ua.Contains("Windows NT 10") ) {
            os_name = "Windows";
            os_version = "10";
            return;
        }

        if ( ua.Contains("Windows NT 6.3") ) {
            os_name = "Windows";
            os_version = "8.1";
            return;
        }

        if ( ua.Contains("Windows NT 6.2") ) {
            os_name = "Windows";
            os_version = "8";
            return;
        }


        if ( ua.Contains("Windows NT 6.1") ) {
            os_name = "Windows";
            os_version = "7";
            return;
        }

        if ( ua.Contains("Windows NT 6.0") ) {
            os_name = "Windows";
            os_version = "Vista";
            return;
        }

        if ( ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2") ) {
            os_name = "Windows";
            os_version = "XP";
            return;
        }

        if ( ua.Contains("Windows NT 5") ) {
            os_name = "Windows";
            os_version = "2000";
            return;
        }

        if ( ua.Contains("Windows NT 4") ) {
            os_name = "Windows";
            os_version = "NT4";
            return;
        }

        if ( ua.Contains("Win 9x 4.90") ) {
            os_name = "Windows";
            os_version = "Me";
            return;
        }

        if ( ua.Contains("Windows 98") ) {
            os_name = "Windows";
            os_version = "98";
            return;
        }

        if ( ua.Contains("Windows 95") ) {
            os_name = "Windows";
            os_version = "95";
            return;
        }


        if ( ua.Contains("Windows Phone") ) {
            os_name = "Windows Phone";
            SetVersion(ua, "Windows Phone");
            return;
        }

        if ( ua.Contains("Linux") && ua.Contains("KFAPWI") ) {os_name = "Kindle Fire";return;}

        if ( ua.Contains("RIM Tablet") || ua.Contains("BB") && ua.Contains("Mobile") ) { os_name = "Black Berry"; return; }
    }

    public string getBrowserName(string browserInfo = "") {
        if ( string.IsNullOrEmpty(browserInfo) )
            return "";
        var browsers = new List<string>() { "Chrome", "Firefox", "Edge", "MSIE", "Opera", "Safari" };
        foreach ( var browserName in browsers ) {
            if ( browserInfo.Contains(browserName) ) {
                var indexFound = browserInfo.IndexOf(browserName);
                var space = browserInfo.IndexOf(" ", indexFound + 1);
                return space > 0 ? browserInfo.Substring(indexFound, space - indexFound).Replace("/", " ") : browserInfo.Substring(indexFound).Replace("/", " ");
            }
        }
        return "";
    }

    private void SetVersion(string userAgent = "", string device = "") {
        if ( string.IsNullOrEmpty(userAgent) || string.IsNullOrEmpty(device) ) {
            os_version = "";
            return;
        }

        var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
        var version = string.Empty;

        foreach ( var character in temp ) {
            var validCharacter = false;
            int test = 0;

            if ( int.TryParse(character.ToString(), out test) ) {
                version += character;
                validCharacter = true;
            }

            if ( character == '.' || character == '_' ) {
                version += '.';
                validCharacter = true;
            }

            if ( validCharacter == false )
                break;
        }
        os_version = version;
    }
}
