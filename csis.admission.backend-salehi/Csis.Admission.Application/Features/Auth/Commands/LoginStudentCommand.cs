using Csis.Admission.Application.Features.Auth.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Auth.Commands;

/// <summary>
/// ورود طلبه به سامانه
/// </summary>
public sealed record LoginStudentCommand : IRequest<LoginResultDto>
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
    /// توکن
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// عکس
    /// </summary>
    public string CaptchaCode { get; set; }
}

internal sealed class LoginStudentCommandHandler : IRequestHandler<LoginStudentCommand, LoginResultDto>
{
    private readonly ICsisAuthorizationService _csisAuthorizationService;
    private readonly IDistributedCacheService _distributedCacheService;

    public LoginStudentCommandHandler(ICsisAuthorizationService csisAuthorizationService,
        IDistributedCacheService distributedCacheService) {
        _csisAuthorizationService = csisAuthorizationService;
        _distributedCacheService = distributedCacheService;
    }

    public async Task<LoginResultDto> Handle(LoginStudentCommand request, CancellationToken cancellationToken) {

        var storedCaptcha = await _distributedCacheService.GetAsync<string>(request.Token, cancellationToken);

        if ( storedCaptcha != request.CaptchaCode ) {
            throw new CommandValidationException(nameof(request.CaptchaCode), "کد امنیتی اشتباه است");
        }

        var tokenResult = await _csisAuthorizationService.LoginStudentAsync(request.Username, request.Password);

        if ( tokenResult?.Succeeded ?? false ) {
            await _distributedCacheService.RemoveAsync(request.Token, cancellationToken);

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
