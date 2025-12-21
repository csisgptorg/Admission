using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// اعتبار سنجی مالکیت شبا
/// </summary>
/// <param name="NationalCode">کد ملی شخص.</param>
/// <param name="AccountNumber">شماره حساب.</param>
public sealed record ValidateShebaOwnershipCommand(string NationalCode, string AccountNumber) : IRequest<ValidateShebaOwnershipResponse>;
internal sealed class ValidateShebaOwnershipCommandHandler(ILogger<ValidateShebaOwnershipCommandHandler> logger, ICsisWsmService csisWsmService)
    : IRequestHandler<ValidateShebaOwnershipCommand, ValidateShebaOwnershipResponse>
{
    public async Task<ValidateShebaOwnershipResponse> Handle(ValidateShebaOwnershipCommand request, CancellationToken cancellationToken) {
        var validateSheba = await csisWsmService.ValidateShebaOwnerShip(request.NationalCode, request.AccountNumber, cancellationToken);

        if ( validateSheba.IsMatched == null || validateSheba.IsMatched == false || validateSheba.ShebaNumber == null ) {
            logger.LogWarning("National code {nationalCode} is not valid according to CSIS WSM", request.NationalCode);
            throw new CommandValidationException(nameof(request.NationalCode), "شماره شبا متعلق به کد ملی وارد شده نمی‌باشد.");
        }

        return validateSheba;
    }
}
