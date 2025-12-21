using Csis.Abstractions.Results;
using Csis.Abstractions.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Csis.Admission;
public partial class AdmissionHttpRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AdmissionHttpRequestService> _logger;
    public AdmissionHttpRequestService(ILogger<AdmissionHttpRequestService> logger, HttpClient httpClient, IOptions<HttpRequestSettings> settings) {
        _logger = logger;
        _httpClient = httpClient;
        HttpClientConfig(settings);
    }

    public async Task SendAsync(HttpRequestMessage request, CancellationToken cancellationToken, [CallerMemberName] string callMember = "") {

        _ = await SendAsync(request, callMember, cancellationToken);
    }

    public async Task<TData> SendAsync<TData>(HttpRequestMessage request, CancellationToken cancellationToken, [CallerMemberName] string callMember = "") {

        try {
            // send request
            var responseContent = await SendAsync(request, callMember, cancellationToken);

            // return data
            return Utilities.Deserialize<Result<TData>>(responseContent).Data;

        } catch ( BadRequestException ) {
            throw;
        } catch ( Exception exception ) {
            _logger.LogError(exception, "Error http request {callMember}", callMember);
            throw new BadRequestException("خطا غیر منتظره در هنگام ساخت پاسخ");
        }
    }

    private async Task<string> SendAsync(HttpRequestMessage request, string callMember, CancellationToken cancellationToken) {

        HttpResponseMessage response;
        callMember = nameof(CsisAdmissionService) + " ---> " + callMember;

        try {

            // log request
            _logger.LogDebug("Sending http request to  {callMember}", callMember);
            _logger.LogDebug("({method}){baseAddress}{uri}", request.Method, _httpClient.BaseAddress, request.RequestUri);

            // send request
            response = await _httpClient.SendAsync(request, cancellationToken);

            // log status
            var status = $"({(int) response.StatusCode}) {response.StatusCode}";
            _logger.LogDebug("Finished sending http request to {callMember} {status}", callMember, status);
            _logger.LogDebug("Response: {response}", Utilities.Serialize(response));

            // read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogDebug("Content: {content}", responseContent);

            // handle response
            if ( !response.IsSuccessStatusCode ) {
                HandleFailureResponse(response);
            }

            // return response content
            return responseContent;
        } catch ( Exception exception ) {
            throw HandleException(exception, callMember);
        }
    }

    private Exception HandleException(Exception exception, string callMember) {
        _logger.LogError(exception, "Error http request {callMember}", callMember);

        if ( exception is HttpRequestException httpRequestException && httpRequestException.HttpRequestError == HttpRequestError.ConnectionError ) {
            return new BadRequestException("ارتباط با سرویس پذیرش برقرار نشد - سرویس در دسترس نمیباشد.");
        }

        if ( exception is BadRequestException ) {
            return exception;
        }

        return new Exception("خطا غیر منتظره در ارسال درخواست");
    }

    public HttpRequestMessage CreateRequest(HttpMethod method, string path, object body = null) {

        var baseUrlPath = new Uri(_settings.BaseUrl).PathAndQuery;
        var fullPath = (baseUrlPath + path).Replace("//", "/");
        var request = new HttpRequestMessage(method, fullPath);
        if ( body != null ) {
            request.Content = Utilities.ToStringContent(body);
        }
        return request;
    }
}


