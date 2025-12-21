using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// Command to validate an Iranian person's identity.
/// </summary>
/// <param name="NationalCode">The national code of the person.</param>
/// <param name="BirthDate">The birthdate of the person.</param>
public sealed record ValidateIranianPersonIdentityCommand(string NationalCode, int BirthDate) : IRequest<GetIdentityInfoByNationalCodeResponse>;

internal sealed class ValidateIranianPersonIdentityCommandHandler(
    ILogger<ValidateIranianPersonIdentityCommandHandler> logger,
    ICsisWsmService csisWsmService)
    : IRequestHandler<ValidateIranianPersonIdentityCommand, GetIdentityInfoByNationalCodeResponse>
{
    public async Task<GetIdentityInfoByNationalCodeResponse> Handle(ValidateIranianPersonIdentityCommand request,
        CancellationToken cancellationToken) {
        var identifyNationalCode = await csisWsmService.GetIdentityInfoByNationalCode(
            new GetIdentityInfoByNationalCodeRequestApiM(request.NationalCode,
                request.BirthDate.ToString()), cancellationToken);

        if ( identifyNationalCode.Nin != null ) {
            if ( identifyNationalCode.DeathStatus == "1" ) {
                throw new CommandValidationException( "امکان ثبت نام برای متوفیان وجود ندارد" );
            }
            identifyNationalCode.IsRegistered = false;
            return identifyNationalCode;
        }

        logger.LogWarning("National code {nationalCode} is not valid according to CSIS WSM", request.NationalCode);
        throw new CommandValidationException(nameof(request.NationalCode), "کد ملی وارد شده نامعتبر است");
    }
}
