/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Abstractions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// اداره خطاهای مربوط به سرویس‌های خارجی
/// </summary>
internal sealed class ServiceClientExceptionHandler(ILogger<ServiceClientExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        if ( exception is BaseServiceException baseServiceException ) {
            logger.LogError(exception, "Error calling '{serviceName}': {serviceMessage}", baseServiceException.ServiceName, baseServiceException.Message);

            var problemDetails = new ProblemDetails {
                Title = "Service Unavailable",
                Detail = baseServiceException.UserFriendlyMessage,
                Instance = httpContext.Request.Path,
                Status = StatusCodes.Status503ServiceUnavailable
            };

            problemDetails.Extensions.Add("traceId", baseServiceException.TraceId);
            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }

        return false;
    }
}
