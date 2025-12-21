using Csis.Admission.Application.Features.ImamJamaat.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;

/// <summary>
/// دریافت آدرس بر اساس کد پستی
/// </summary>
/// <param name="PostalCode"></param>
public sealed record GetAddressByPostalCodeQuery(long PostalCode) : IRequest<MosqueAddressFromExternalServiceDto>;

internal sealed class GetAddressByPostalCodeQueryHandler : IRequestHandler<GetAddressByPostalCodeQuery, MosqueAddressFromExternalServiceDto>
{
    private readonly ICsisWsmService _wsmService;
    private readonly IMapper _mapper;

    public GetAddressByPostalCodeQueryHandler(ICsisWsmService wsmService, IMapper mapper) {
        _wsmService = wsmService;
        _mapper = mapper;
    }
    public async Task<MosqueAddressFromExternalServiceDto> Handle(GetAddressByPostalCodeQuery request, CancellationToken cancellationToken) {

        try {
            var addressByPostalCode = await _wsmService.GetAddressByPostalCode(0,request.PostalCode, cancellationToken);
            var localApplicationModelAddress = addressByPostalCode.GetAddress(0, request.PostalCode);
            var address = localApplicationModelAddress.ToEntity();
            return _mapper.Map<MosqueAddressFromExternalServiceDto>(address);
        } catch ( Exception e ) {
            throw new BadRequestException( $"آدرس با کد پستی {request.PostalCode} یافت نشد.");
        }

    }
}
