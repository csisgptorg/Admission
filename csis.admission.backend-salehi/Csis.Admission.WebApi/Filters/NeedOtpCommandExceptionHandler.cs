using Csis.Abstractions.Results;
using Microsoft.AspNetCore.Diagnostics;
using Csis.Admission.Application.Common;

namespace Csis.Admission.WebApi.Filters;

/// <summary>تایید کد یکبار مصرف</summary>
internal sealed class NeedOtpCommandExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

        if ( exception is NeedOtpCommandException otpException) {
            var result = Result<object>.Success(new { otpException.ExpiresInSeconds }, otpException.Message);
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);

            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            return true;
        }

        return false;
    }
}
