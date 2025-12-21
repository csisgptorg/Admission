using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// Command to validate a non-Iranian person's identity.
/// </summary>
/// <param name="YektaCode">The Yekta code of the person.</param>
public sealed record ValidateNonIranianPersonIdentityCommand(string YektaCode) : IRequest<ValidateNonIranianYektaCodeResponse>;

internal sealed class ValidateNonIranianPersonIdentityCommandHandler(
    ILogger<ValidateNonIranianPersonIdentityCommandHandler> logger,
    ICsisWsmService csisWsmService)
    : IRequestHandler<ValidateNonIranianPersonIdentityCommand, ValidateNonIranianYektaCodeResponse>
{
    // Response is a property of ValidateNonIranianYektaCodeResponse
    public async Task<ValidateNonIranianYektaCodeResponse> Handle(ValidateNonIranianPersonIdentityCommand request, CancellationToken cancellationToken) {
        var identifyYektaCode = await csisWsmService.ValidateNonIranianYektaCode(-1, request.YektaCode, cancellationToken);

        if ( !identifyYektaCode.IsValid() ) {
            logger.LogWarning("Yekta code {yektaCode} is not valid according to CSIS WSM", request.YektaCode);
            throw new CommandValidationException(nameof(request.YektaCode), "شناسه یکتا وارد شده نامعتبر است");
        }
        identifyYektaCode.IsRegistered = false;
        return identifyYektaCode;
    }
}
