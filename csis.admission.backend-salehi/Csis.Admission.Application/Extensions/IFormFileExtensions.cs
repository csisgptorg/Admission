using Microsoft.AspNetCore.Http;

namespace Csis.Admission.Application.Extensions;

/// <summary>
/// 
/// </summary>
public static class IFormFileExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static async Task<byte[]> ToByteArray(this IFormFile file) {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
