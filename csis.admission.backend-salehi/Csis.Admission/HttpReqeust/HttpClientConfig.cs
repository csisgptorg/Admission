using Csis.Abstractions.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Csis.Admission;
public partial class AdmissionHttpRequestService
{
    private HttpRequestSettings _settings;
    private void HttpClientConfig(IOptions<HttpRequestSettings> httpRequestSettings) {
        _settings = httpRequestSettings.Value ??
            throw new ArgumentNullException(nameof(HttpRequestSettings) + "Options");

        if ( string.IsNullOrWhiteSpace(_settings.BaseUrl) )
            throw new BadRequestException("آدرس پایه نمیتواند خالی باشد.");

        if ( string.IsNullOrWhiteSpace(_settings.ApiKey) )
            throw new BadRequestException("کلید احراز هویت نمیتواند خالی باشد.");

        var baseUrl = new Uri(_settings.BaseUrl);
        _httpClient.BaseAddress = baseUrl;
        _logger.LogDebug("HttpClient BaseAddress with {baseAddress}", _httpClient.BaseAddress);

        _httpClient.Timeout = TimeSpan.FromSeconds(Math.Abs(_settings.TimeoutInSeconds));
        _logger.LogDebug("HttpClient Timeout with {timeout}", _httpClient.Timeout);

        _httpClient.DefaultRequestHeaders.Add("X-API-Key", _settings.ApiKey);
        _logger.LogDebug("HttpClient Header X-API-Key with {apiKey}", _settings.ApiKey);
    }
}
