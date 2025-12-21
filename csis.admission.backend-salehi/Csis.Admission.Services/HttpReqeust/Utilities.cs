using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Csis.Admission.Services;
public static class Utilities
{
    private static readonly JsonSerializerOptions _stringContentOptions = new() {
        MaxDepth = 16,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    public static StringContent ToStringContent(object obj) {
        if(obj==null ) {
            return null;
        }

        var json = JsonSerializer.Serialize(obj, _stringContentOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private static readonly JsonSerializerOptions _jsonOptions = new() {
        MaxDepth = 16,
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public static TResult Deserialize<TResult>(string json) {
        var result = JsonSerializer.Deserialize<TResult>(json, _jsonOptions);
        return result;
    }
    
    public static string Serialize(object obj) {
        var json = JsonSerializer.Serialize(obj, _jsonOptions);
        return json;
    }

    public static bool ValidateUrl(string url) {
        var urlRegex = new Regex(
            @"^(https?|ftps?):\/\/(?:[a-zA-Z0-9]" +
            @"(?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}" +
            @"(?::(?:0|[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}" +
            @"|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?" +
            @"(?:\/(?:[-a-zA-Z0-9@%_\+.~#?&=]+\/?)*)?$",
            RegexOptions.IgnoreCase);

        urlRegex.Matches(url);

        return urlRegex.IsMatch(url);
    }
}
