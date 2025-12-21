using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Csis.Admission;
public static class Utilities
{
    private static readonly JsonSerializerOptions _stringContentOptions = new() {
        MaxDepth = 16,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    public static StringContent ToStringContent(object obj) {
        if ( obj == null ) {
            return null;
        }

        var json = JsonSerializer.Serialize(obj, _stringContentOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public static TResult Deserialize<TResult>(string json) {
        var result = JsonSerializer.Deserialize<TResult>(json, _jsonOptions);
        return result;
    }

    private static readonly JsonSerializerOptions _jsonOptions = new() {
        MaxDepth = 16,
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    public static string Serialize(object obj) {
        var json = JsonSerializer.Serialize(obj, _jsonOptions);
        return json;
    }
}
