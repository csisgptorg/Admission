using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Enums;
using Microsoft.Extensions.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Microsoft.Extensions.Logging;
using Csis.Utilities.Extensions;

namespace Csis.Admission.Services;
internal sealed partial class CsisWsmService : ICsisWsmService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<CsisWSMLog, long> _logRepo;
    private readonly IHttpRequestService _httpRequestService;
    private readonly IStudentRepository _studentRepo;
    private readonly ILogger<CsisWsmService> _logger;

    public CsisWsmService(IRepository<CsisWSMLog, long> logRepo, IConfiguration configuration, IHttpRequestService httpRequestService, 
        IStudentRepository studentRepository, ILogger<CsisWsmService> logger) {
        _logRepo = logRepo;
        _configuration = configuration;
        _httpRequestService = httpRequestService;
        _studentRepo = studentRepository;
        _logger = logger;
    }

    private async Task<AuthenticationHeaderValue> GetToken(CancellationToken cancellation) {
        var path = "/api/v1/auth/token";
        var request = _configuration.GetSection("CsisWsm").Get<LoginRequest>();

        var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
        var httpResult = await _httpRequestService.SendAsync<LoginResponse>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation);

        if ( httpResult.Succeeded == false ) {
            throw new BadRequestException(httpResult.Message + "\n" + httpResult.Response);
        }

        return new AuthenticationHeaderValue("bearer", httpResult.ApiResult.Data.Access_Token);
    }

    private async Task<AuthenticationHeaderValue> GetTokenApi(CancellationToken cancellation) {

        //TODO بیس داخل اپ ستیگ باشه
        var path = "/oauth2/token";
        var options = _configuration.GetSection("CsisWsmApi").Get<LoginApiRequest>();
        _logger.LogInformation("CsisWsmApi Login options: {options}", options.ToJson());

        //TODO از اپ ستینگ خوانده شود
        var client = Encoding.ASCII.GetBytes($"{options.ClientId}:{options.ClientSecret}");
        _logger.LogInformation("CsisWsmApi Login options: {options}", client.ToJson());
        var authenticationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(client));
        _logger.LogInformation("CsisWsmApi Login options: {authenticationHeader}", authenticationHeader.ToJson());

        //TODO از اپ ستینگ خوانده شود
        var body = new { grant_type = "password", username = options.Username , password = options.Password };
        _logger.LogInformation("CsisWsmApi Login options: {body}", body.ToJson());
        var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, body);
        _logger.LogInformation("CsisWsmApi Login options: {httpRequest}", httpRequest.ToJson());
        var httpResult = await _httpRequestService.SendAsync<LoginResponse.Result>
            (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, authenticationHeader);
        _logger.LogInformation("CsisWsmApi Login options: {httpResult}", httpResult.ToJson());
        if ( httpResult.Succeeded == false ) {
            throw new BadRequestException(httpResult.Message + "\n" + httpResult.Response);
        }

        return new AuthenticationHeaderValue("Bearer", httpResult.ApiResult.Access_Token);
    }

    public record LoginRequest(string Username, string Password);
    public record LoginApiRequest(string ClientId,string ClientSecret, string Username, string Password);

    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public Result Data { get; set; }
        public ResponseExtra Extra { get; set; }

        public record ResponseExtra(string Message);
        public record Result(string Access_Token, string Token_Type, int Expires_In);
    }

    private static readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private static TType Deserialize<TType>(string json) => JsonSerializer.Deserialize<TType>(json, _serializerOptions);
}
