using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class TraceLog : BaseEntity<long>
{
    /// <inheritdoc/>
    public TraceLog(string traceId, string url,string data, string type, int? duration = null,int? statusCode=null) {
        TraceId = traceId;
        Url = url;
        Data =string.IsNullOrWhiteSpace(data) || url.Contains("auth/login") ?null: data;
        Type = type;
        Duration = duration;
        StatusCode = statusCode;
    }

    private TraceLog() { }

    /// <inheritdoc/>
    public string TraceId { get; private set; }

    /// <inheritdoc/>
    public string Url { get; private set; }

    /// <inheritdoc/>
    public int? StatusCode { get; private set; }

    /// <inheritdoc/>
    [JsonConverter(typeof(RawJsonConverter))]
    public string Data { get; set; }

    /// <inheritdoc/>
    public string Type { get; private set; }

    /// <inheritdoc/>
    public int? Duration { get; set; }

    /// <inheritdoc/>
    public void SetDuration()=>Duration=(DateTime.Now - CreatedOn).Milliseconds;
}

/// <inheritdoc/>
public class RawJsonConverter : JsonConverter<string>
{
    /// <summary> </summary>
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return reader.GetString()!;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) {
        writer.WriteRawValue(value);
    }
}
