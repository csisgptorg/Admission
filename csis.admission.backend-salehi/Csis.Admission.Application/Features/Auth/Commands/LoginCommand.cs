using Csis.Admission.Application.Features.Auth.Dtos;
using Csis.Authorization.Models;
using Csis.Authorization.Services;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Auth.Commands;

/// <summary>
/// ورود به سامانه
/// </summary>
public sealed record LoginCommand : IRequest<LoginResultDto>
{
    /// <summary>
    /// نام کاربری
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// کلمه عبور
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// توکن دریافت شده از اپ های خارجی مانند سخا
    /// </summary>
    [JsonPropertyName("token")]
    public string ExternalToken { get; set; }

    /// <summary>
    /// نوع کاربر
    /// </summary>
    public UserType UserType { get; set; }
}

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
{
    private readonly ICsisAuthorizationService _csisAuthorizationService;

    public LoginCommandHandler(ICsisAuthorizationService csisAuthorizationService) {
        _csisAuthorizationService = csisAuthorizationService;
    }

    public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var tokenResult = request.UserType == UserType.Employee?
            await _csisAuthorizationService.LoginAsync(request.Username, request.Password) :
            await _csisAuthorizationService.LoginStudentAsync(request.ExternalToken);

        if ( tokenResult?.Succeeded ?? false ) {
            return new LoginResultDto {
                TokenInfo = tokenResult.Data,
                Succeeded = true
            };
        } else {
            return new LoginResultDto {
                ErrorMessage = tokenResult.Message,
                Succeeded = false
            };
        }
    }
}
