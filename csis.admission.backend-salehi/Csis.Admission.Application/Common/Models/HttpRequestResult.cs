using System.Net;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public sealed class HttpRequestResult<TApiResult>
{
    /// <inheritdoc/>
    public HttpStatusCode StatusCode { get; set; }

    /// <inheritdoc/>
    public bool Succeeded { get; set; } = false;

    /// <inheritdoc/>
    public string Message { get; set; }

    /// <inheritdoc/>
    public TApiResult ApiResult { get; set; }

    /// <inheritdoc/>
    public string Response { get; set; }
};
