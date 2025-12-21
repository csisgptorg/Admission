using System.Net.Http.Headers;
using Csis.Abstractions.Exceptions;
using Microsoft.Extensions.Logging;
using Csis.Admission.Application.Enums;
using Microsoft.Extensions.Configuration;

namespace Csis.Admission.Services;
public partial class HttpRequestService
{
    private HttpRequestOptions _httpRequestOptions;

    private HttpClient HttpClientConfig(HttpRequestSectionOptions sectionOption, AuthenticationHeaderValue authenticationHeader = null) {

        // section option
        var sectionName = sectionOption.ToString();
        _httpRequestOptions = Configuration.GetSection(sectionName).Get<HttpRequestOptions>();
        if ( _httpRequestOptions == null ) {
            throw new BadRequestException(sectionName + $" ==> تنظیمات ({sectionName}) یافت نشد - نامعتبر است.");
        }

        // validate base url
        if ( string.IsNullOrWhiteSpace(_httpRequestOptions.BaseUrl) ) {
            throw new BadRequestException(sectionName + " ==> آدرس پایه نمیتواند خالی باشد.");
        }

        // validate base url
        if ( !Utilities.ValidateUrl(_httpRequestOptions.BaseUrl) ) {
            throw new BadRequestException(sectionName + $" ==> آدرس پایه ({_httpRequestOptions.BaseUrl}) نامعتبر است.");
        }

        // validate api key
        if ( string.IsNullOrWhiteSpace(_httpRequestOptions.ApiKey) 
            && !_httpRequestOptions.BaseUrl.Contains("wsm.csis.ir") && !_httpRequestOptions.BaseUrl.Contains("api.csis.ir") ) {
            throw new BadRequestException(sectionName + " ==> کلید احراز هویت نمیتواند خالی باشد.");
        }

        // configure http client
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(_httpRequestOptions.BaseUrl);
        _logger.LogDebug("HttpClient BaseAddress with {baseAddress}", httpClient.BaseAddress);

        httpClient.Timeout = TimeSpan.FromSeconds(Math.Abs(_httpRequestOptions.TimeoutInSeconds));
        _logger.LogDebug("HttpClient Timeout with {timeout}", httpClient.Timeout);

        //TODO بهینه شود! هارد کد نداشته باشیم
        if ( authenticationHeader != null ) {
            httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;
        }
        if ( !_httpRequestOptions.BaseUrl.Contains("wsm.csis.ir") || !_httpRequestOptions.BaseUrl.Contains("api.csis.ir") ) {
            httpClient.DefaultRequestHeaders.Add("X-API-Key", _httpRequestOptions.ApiKey);
            _logger.LogDebug("HttpClient Header X-API-Key with {apiKey}", _httpRequestOptions.ApiKey);
        }

        return httpClient;
    }
}
