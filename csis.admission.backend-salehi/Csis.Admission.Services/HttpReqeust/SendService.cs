using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces;

namespace Csis.Admission.Services;
public partial class HttpRequestService : IHttpRequestService
{
    public IConfiguration Configuration;
    private readonly ILogger<HttpRequestService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public HttpRequestService(ILogger<HttpRequestService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory) {
        _logger = logger;
        Configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpRequestResult<TApiResult>> SendAsync<TApiResult>(HttpRequestSectionOptions sectionOption, HttpRequestMessage request,
        CancellationToken cancellationToken, AuthenticationHeaderValue authenticationHeader = null, [CallerMemberName] string callMember = "") {

        callMember = sectionOption.ToString() + " ---> " + callMember;
        HttpRequestResult<TApiResult> result = new();

        try {
            // HttpClientConfig
            var httpClient = HttpClientConfig(sectionOption, authenticationHeader);

            // log request
            _logger.LogInformation("\n\n\nSending http request to {callMember}", callMember);
            _logger.LogInformation("({method}){uri}\n\n", request.Method, httpClient.BaseAddress.OriginalString+ request.RequestUri.OriginalString);
            _logger.LogInformation("(Authorization: {token}\n\n", httpClient.DefaultRequestHeaders.Authorization);
            _logger.LogInformation("(X-API-Key: {apiKey}\n\n", httpClient.DefaultRequestHeaders.Where(x => x.Key == "X-API-Key").Select(x => x.Value).SingleOrDefault());

            var fullRequestPath = (httpClient.BaseAddress.LocalPath+ request.RequestUri.OriginalString).Replace("//","/");
            request.RequestUri = new Uri(fullRequestPath, UriKind.Relative);

            if ( request.Content != null ) {
                var requestBody = await request.Content?.ReadAsStringAsync();
                _logger.LogInformation("Request Body:{body}\n\n", requestBody);
            }

            // send request
            var response = await httpClient.SendAsync(request, cancellationToken);

            // log status
            var status = $"({(int) response.StatusCode}) {response.StatusCode}";
            _logger.LogInformation("Finished sending http request to {callMember} {status}\n\n", callMember, status);
            //_logger.LogInformation("Response: {response}\n\n", Utilities.Serialize(response));

            // read response
            result.Response = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Content: {content}\n\n\n", result.Response);
            result.StatusCode = response.StatusCode;

            // return response
            if ( response.IsSuccessStatusCode ) {
                result.Succeeded = true;
                if ( response.StatusCode != HttpStatusCode.NoContent ) {
                    result.ApiResult = Utilities.Deserialize<TApiResult>(result.Response);
                }
            } else {
                result.Message = HandleFailureResponse(response);
            }

        } catch ( Exception exception ) {
            result = HandleException<TApiResult>(exception, callMember);
        }

        return result;
    }

    public static HttpRequestMessage CreateRequest(HttpMethod method, string path, object body = null) {
        var request = new HttpRequestMessage(method, path) {
            Content = Utilities.ToStringContent(body)
        };
        return request;
    }
}
