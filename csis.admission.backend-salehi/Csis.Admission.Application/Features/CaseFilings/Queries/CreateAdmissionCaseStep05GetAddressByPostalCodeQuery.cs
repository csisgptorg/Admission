using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.CaseFilings.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Queries;

/// <summary>
/// </summary>
/// <param name="Token"></param>
/// <param name="PostalCode"></param>
public sealed record GetAddressByPostalCodeQuery(Guid Token, long PostalCode) : IRequest<AddressFromExternalServiceDto>;

internal sealed class GetAddressByPostalCodeQueryHandler(
    ICsisWsmService wsmService,
    IRepository<AdmissionCaseUser, Guid> userRepository)
    : IRequestHandler<GetAddressByPostalCodeQuery, AddressFromExternalServiceDto>
{
    public async Task<AddressFromExternalServiceDto> Handle(GetAddressByPostalCodeQuery request, CancellationToken cancellationToken) {

        if ( !await userRepository.ExistsAsync(x => x.Id == request.Token, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("شناسه نامعتبر است.");
        }

        var response = await wsmService.GetAddressByPostalCode(-1, request.PostalCode, cancellationToken);
        if ( !response.IsAddressFound ) {
            throw new CommandValidationException("کد پستی نامعتبر است.");
        }
        var localApplicationModelAddress = response.GetAddress(-1, request.PostalCode);
        var address = localApplicationModelAddress.ToEntity().MapTo<AddressFromExternalServiceDto>();

        return address;
    }
}
