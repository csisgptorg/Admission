using System.Net;
using Microsoft.Extensions.Logging;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Services;
public partial class HttpRequestService
{
    private string HandleFailureResponse(HttpResponseMessage response) {
        _logger.LogError("{handler} // StatusCode:{statusCode} // ResponseContetn:{content}\n\n\n",
            nameof(HandleFailureResponse), response.StatusCode, response.Content.ReadAsStringAsync());

        return response.StatusCode switch {
            HttpStatusCode.Unauthorized => "خطای احرازهویت",
            HttpStatusCode.Forbidden => "خطای دسترسی",
            HttpStatusCode.NotFound => "یافت نشد.",
            _ => $"({(int) response.StatusCode}) ناموفق"
        };
    }

    private HttpRequestResult<TApiResult> HandleException<TApiResult>(Exception exception, string callMember) {
        _logger.LogError(exception, "Error http request {callMember}\n\n\n", callMember);

        var result = new HttpRequestResult<TApiResult> { StatusCode = HttpStatusCode.BadRequest };
        if ( exception is HttpRequestException connectionException && connectionException.HttpRequestError == HttpRequestError.ConnectionError ) {
            result.Message = "ارتباط با سرویس بیرونی برقرار نشد - سرویس در دسترس نمیباشد.";
        } else if ( exception is HttpRequestException sslException && sslException.HttpRequestError == HttpRequestError.SecureConnectionError ) {
            result.Message = "ارتباط با سرویس بیرونی برقرار نشد - خطای ارتباط امن (SSL).";
        } else if ( exception is BadRequestException ) {
            result.Message = exception.Message;
        } else {
            result.StatusCode = HttpStatusCode.InternalServerError;
            result.Message = "خطا غیر منتظره در ارسال درخواست";
        }

        return result;
    }
}
