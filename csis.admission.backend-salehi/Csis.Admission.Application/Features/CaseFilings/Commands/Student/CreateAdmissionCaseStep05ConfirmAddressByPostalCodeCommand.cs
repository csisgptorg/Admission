using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.CaseFilings.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// تایید آدرس بر اساس کدپستی
/// </summary>
public sealed record ConfirmAddressByPostalCodeCommand : IRequest
{
    public Guid Token { get; init; }
    public long PostalCode { get; init; }
    public AddressFromExternalServiceDto Address { get; init; }
}

internal sealed class ConfirmAddressByPostalCodeCommandHandler(IRepository<AdmissionCaseUser, Guid> userRepository) : IRequestHandler<ConfirmAddressByPostalCodeCommand>
{
    public async Task Handle(ConfirmAddressByPostalCodeCommand request, CancellationToken cancellationToken) {

        var admissionCaseUser = await userRepository.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");

        admissionCaseUser.PostalCode = request.PostalCode;
        admissionCaseUser.CaseStep = AdmissionCaseStep.AddressVerified;
        admissionCaseUser.Payloads = PayloadHelper.AddPayloadsToString(request.Address, admissionCaseUser.Payloads, nameof(AdmissionCasePayloadName.Address));
        await userRepository.UpdateAsync(admissionCaseUser, cancellationToken: cancellationToken);
    }
}
