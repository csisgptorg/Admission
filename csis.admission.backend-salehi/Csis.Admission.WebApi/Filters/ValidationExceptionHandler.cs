/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// اداره خطای اعتبارسنجی
/// </summary>
/// <param name="logger"></param>
internal sealed class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        if ( exception is ValidationException validationException ) {
            var traceId = Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier;
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            var problemDetails = new ValidationProblemDetails(errors) {
                Title = "Validation failed.",
                Instance = httpContext.Request.Path,
                Status = StatusCodes.Status422UnprocessableEntity
            };

            problemDetails.Extensions.Add("traceId", traceId);
            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            logger.LogInformation("Validation failed: {@validationErrors}", errors);

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }

        return false;
    }
}
