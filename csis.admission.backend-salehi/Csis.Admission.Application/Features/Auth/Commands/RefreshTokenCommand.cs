using Csis.Admission.Application.Features.Auth.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Auth.Commands;

/// <summary>
/// رفرش توکن
/// </summary>
/// <param name="JwToken"></param>
/// <param name="RefreshToken"></param>
public sealed record RefreshTokenCommand(string JwToken, string RefreshToken) : IRequest<LoginResultDto>;

internal sealed class RefreshTokenCommandHandler(ICsisAuthorizationService csisAuthorizationService) : IRequestHandler<RefreshTokenCommand, LoginResultDto>
{
    public async Task<LoginResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken) {
        var tokenResult = await csisAuthorizationService.RefreshTokenAsync(request.JwToken, request.RefreshToken);

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
