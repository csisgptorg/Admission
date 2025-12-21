/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// Handles all unhandled exceptions
/// </summary>
internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const string TraceIdKey = "traceId";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        var traceId = Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier;
        ProblemDetails problemDetails = null;
        ValidationProblemDetails validationProblemDetails = null;

        if ( exception is RecordNotFoundException ) {

            logger.LogWarning(Events.NotFound, exception, "Record not found");

            problemDetails = new ProblemDetails {
                Title = "Resource not found",
                Status = StatusCodes.Status404NotFound,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            };

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        } else if ( exception is CommandValidationException commandValidationException ) {

            validationProblemDetails = new ValidationProblemDetails {
                Title = "Validation failed",
                Status = StatusCodes.Status422UnprocessableEntity,
                Errors = commandValidationException.Messages?.Length > 0 ?
                    new Dictionary<string, string[]> {
                        { commandValidationException.PropertyName ?? "", commandValidationException.Messages }
                    } :
                    new Dictionary<string, string[]> {
                        { commandValidationException.PropertyName ?? "", new string[] { commandValidationException.Message } }
                    }
            };

            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        } else if ( exception is UnauthorizedActionException unauthorizedActionException ) {

            problemDetails = new ProblemDetails {
                Title = unauthorizedActionException.HasCustomMessage ? unauthorizedActionException.Message : "Unauthorized action",
                Status = StatusCodes.Status403Forbidden,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            };

            logger.LogWarning(Events.UnauthorizedAction, exception, "Unauthorized action detected");
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

        } else if ( exception is PagingArgumentException pagingException ) {

            validationProblemDetails = new ValidationProblemDetails {
                Title = "Validation failed",
                Status = StatusCodes.Status422UnprocessableEntity,
                Errors = new Dictionary<string, string[]> {
                    { "sortBy", new string[] { $"The value '{pagingException.SortParamName}' is invalid for sort." } }
                }
            };

            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        } else if ( exception is NotImplementedException ) {

            problemDetails = new ProblemDetails {
                Title = "Feature not implemented",
                Status = StatusCodes.Status501NotImplemented,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.2",
            };

            logger.LogError(Events.NotImplemented, exception, "Feature not implemented");
            httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;

        } else if ( exception is BadRequestException ) {

            problemDetails = new ProblemDetails {
                Title = "Bad request",
                Detail = exception.Message,
                Status = StatusCodes.Status400BadRequest,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            };

            logger.LogError(Events.BadRequest, exception, "Bad request");
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        } else if ( exception is TimeoutException ) {

            problemDetails = new ProblemDetails {
                Title = "Request timed out",
                Detail = "به علت بار سنگین سمت سرور امکان پردازش درخواست شما وجود ندارد. لطفا مجددا تلاش نمایید.",
                Status = StatusCodes.Status503ServiceUnavailable,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4",
            };

            logger.LogError(Events.ServiceUnavailable, exception, "Request timed out");
            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;

        } else {

            problemDetails = new ProblemDetails {
                Title = "Internal server error",
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            };

            logger.LogError(Events.UnhandledException, exception, "An unhandled exception occurred");

        }

        if ( validationProblemDetails is not null ) {
            validationProblemDetails.Instance = httpContext.Request.Path;
            validationProblemDetails.Extensions.Add(TraceIdKey, traceId);

            await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken: cancellationToken);
            return true;
        }

        if ( problemDetails is not null ) {
            problemDetails.Instance = httpContext.Request.Path;
            problemDetails.Extensions.Add(TraceIdKey, traceId);

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }

        return false;
    }
}
