using Csis.Abstractions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// اداره خطاهای مربوط به بیزینس
/// </summary>
internal sealed class BusinessFlowExceptionsHandler(ILogger<BusinessFlowExceptionsHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        var traceId = Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier;
        string message = null;

        if ( exception is EmptyBranchIdException ) {
            message = "کد شعبه کاربری شما ثبت نشده است";
            logger.LogError(exception, "Empty branch id");
        } else if ( exception is UnAuthorizedBranchException ) {
            message = "دسترسی به اطلاعات این شعبه مجاز نیست";
            logger.LogError(exception, "Unauthorized branch access");
        }

        if ( message is not null ) {
            var problemDetails = new ProblemDetails {
                Title = "Conflict",
                Detail = message,
                Instance = httpContext.Request.Path,
                Status = StatusCodes.Status409Conflict
            };

            problemDetails.Extensions.Add("traceId", traceId);
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }

        return false;
    }
}
