using Csis.Abstractions.Results;
using Microsoft.AspNetCore.Diagnostics;
using Csis.Admission.Application.Common;

namespace Csis.Admission.WebApi.Filters;

/// <summary>تایید اطلاعات</summary>
internal sealed class ConfirmedValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

        if ( exception is ConfirmedValidationException confirmedValidation ) {
            var result = Result<object>.Success(confirmedValidation.Data);
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);

            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            return true;
        }

        return false;
    }
}
