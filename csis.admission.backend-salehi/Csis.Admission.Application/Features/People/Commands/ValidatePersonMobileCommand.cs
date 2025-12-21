using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// اعتبارسنجی شماره همراه شخص
/// </summary>
public sealed record ValidatePersonMobileCommand(string NationalCode, string Mobile) : IRequest<bool>;

internal sealed class ValidatePersonMobileCommandHandler(
    ICsisWsmService csisWsmService,
    ILogger<ValidatePersonMobileCommandHandler> logger)
    : IRequestHandler<ValidatePersonMobileCommand, bool>
{
    public async Task<bool> Handle(ValidatePersonMobileCommand request, CancellationToken cancellationToken)
    {
        logger.LogDebug("Validating mobile ownership for national code: {NationalCode}, mobile: {Mobile}", 
            request.NationalCode, request.Mobile);

        var validationRequest = new ValidateMobileOwnershipRequest(request.NationalCode, request.Mobile);
        var isValid = await csisWsmService.ValidateMobileOwnership(validationRequest, cancellationToken);

        logger.LogDebug("Mobile validation result: {IsValid}", isValid);

        if (!isValid)
        {
            throw new CommandValidationException("شماره همراه معتبر نمی باشد.");
        }

        return isValid;
    }
}
