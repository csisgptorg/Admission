using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Shared.Kernel.Public.Extensions;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// احراز هویت
/// </summary>
public sealed record ValidateIdentityCommand(Guid Token) : IRequest<ValidateIdentityDto>;

internal sealed class ValidateIdentityCommandHandler(ILogger<ValidateIdentityCommandHandler> logger, IRepository<AdmissionCaseUser, Guid> repository, ICsisWsmService csisWsmService)
    : IRequestHandler<ValidateIdentityCommand, ValidateIdentityDto>
{
    public async Task<ValidateIdentityDto> Handle(ValidateIdentityCommand request, CancellationToken cancellationToken) {

        var caseUser = await repository.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("شناسه نامعتبر است.");

        switch ( caseUser.Citizenship ) {
            case Citizenship.Iranian: {
                var identifyNationalCode = await csisWsmService.GetIdentityInfoByNationalCode(
                    new GetIdentityInfoByNationalCodeRequest(-1, caseUser.NationalCode, caseUser.BirthDate),
                    cancellationToken
                );

                if ( string.IsNullOrEmpty(identifyNationalCode.Nin) ) {
                    logger.LogWarning("National code {nationalCode} is not valid according to CSIS WSM", caseUser.NationalCode);
                    throw new CommandValidationException(nameof(caseUser.NationalCode), "کد ملی وارد شده نامعتبر است");
                }

                var result = new ValidateIdentityDto(identifyNationalCode.Name, identifyNationalCode.Family, identifyNationalCode.FatherName, Citizenship.Iranian, identifyNationalCode.BirthDate, caseUser.NationalCode, Gender: (Gender) identifyNationalCode.Gender.ToInt());
                caseUser.Payloads = PayloadHelper.AddPayloadsToString(result, caseUser.Payloads,
                    nameof(AdmissionCasePayloadName.Identity));

                await repository.UpdateAsync(caseUser, true, cancellationToken);
                return result;
            }
            case Citizenship.NonIranian: {
                var identifyYektaCode = await csisWsmService.ValidateNonIranianYektaCode(-1, caseUser.YektaCode, cancellationToken
                );

                if ( !identifyYektaCode.IsValid() ) {
                    logger.LogWarning("Yekta code {yektaCode} is not valid according to CSIS WSM", caseUser.YektaCode);
                    throw new CommandValidationException(nameof(caseUser.YektaCode), "شماره یکتا وارد شده نامعتبر است");
                }

                var result = new ValidateIdentityDto(identifyYektaCode.FirstName, identifyYektaCode.LastName,
                    identifyYektaCode.FatherName, Citizenship.NonIranian,
                    identifyYektaCode.BirthDate.ToPersianDateOnly(), YektaCode: caseUser.YektaCode, Gender: (Gender) identifyYektaCode.Gender);

                caseUser.Payloads = PayloadHelper.AddPayloadsToString(result, caseUser.Payloads,
                    nameof(AdmissionCasePayloadName.Identity));
                await repository.UpdateAsync(caseUser, true, cancellationToken);
                return result;
            }
            default:
                throw new CommandValidationException(nameof(caseUser.Citizenship), "نوع تابعیت نامعتبر است");
        }
    }
}
