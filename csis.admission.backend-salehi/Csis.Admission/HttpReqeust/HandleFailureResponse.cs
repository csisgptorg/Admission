using System.Net;
using Csis.Abstractions.Exceptions;
using Microsoft.Extensions.Logging;

namespace Csis.Admission;
public partial class AdmissionHttpRequestService
{
    private void HandleFailureResponse(HttpResponseMessage response) {
        _logger.LogError("{handler} // StatusCode:{statusCode} // ResponseContetn:{content}",
            nameof(HandleFailureResponse), response.StatusCode, response.Content.ReadAsStringAsync());

        if ( response.StatusCode == HttpStatusCode.InternalServerError ) {
            throw new Exception("خطای غیره منتظره!");
        } else {
            var message = response.StatusCode switch {
                HttpStatusCode.Unauthorized => "خطای احرازهویت",
                HttpStatusCode.Forbidden => "خطای دسترسی",
                HttpStatusCode.NotFound => "یافت نشد.",
                _ => $"({(int) response.StatusCode}) ناموفق"
            };

            throw new BadRequestException(message);
        }
    }
}
