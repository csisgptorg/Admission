using System.Text.Json;

namespace Csis.Admission.Application.Common.Helpers;

public static class PayloadHelper
{
    public sealed record NamedPayload(string Name, object Payload);


    /// <summary>
    /// افزودن پیلود جدید به رشته پیلودهای موجود
    /// </summary>
    /// <param name="newPayload"></param>
    /// <param name="existingPayload"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string AddPayloadsToString(object newPayload, string existingPayload, string name) {
        if ( string.IsNullOrWhiteSpace(existingPayload) ) {
            var payload = new List<NamedPayload> { new(name ?? "Payload", newPayload) };
            return JsonSerializer.Serialize(payload);
        }
        var payloads = JsonSerializer.Deserialize<List<NamedPayload>>(existingPayload);

        if(payloads.Any(x=>x.Name == name)) {
            payloads.RemoveAll(x => x.Name == name);
        }

        payloads.Add(new(name ?? "Payload", newPayload));
        return JsonSerializer.Serialize(payloads);
    }

    /// <summary>
    /// استخراج پیلودها از رشته پیلودهای موجود
    /// </summary>
    /// <param name="existingPayload"></param>
    /// <returns></returns>
    public static List<NamedPayload> GetPayloadFromString(string existingPayload) {
        if ( string.IsNullOrWhiteSpace(existingPayload) ) {
            return null;
        }
        var payloads = JsonSerializer.Deserialize<List<NamedPayload>>(existingPayload);
        return payloads;
    }
}
