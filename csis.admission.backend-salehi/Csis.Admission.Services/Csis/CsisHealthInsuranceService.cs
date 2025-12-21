using Csis.Abstractions.Results;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Services;
internal sealed partial class CsisHealthInsuranceService : ICsisHealthInsuranceService
{
    private readonly IHttpRequestService _httpRequestService;
    public CsisHealthInsuranceService(IHttpRequestService httpRequestService) {
        _httpRequestService = httpRequestService;
    }

    /// <inheritdoc/>
    public async Task<CurrentHealthInsuranceCaseStateDto> CaseState(int codm, long? dependentId, CancellationToken cancellation) {
        var path = "/api/private/external/case-state/"+codm;
        if ( dependentId > 0 ) {
            path += "?takafolId="+ dependentId;
        }

        var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Get, path);
        var httpResult = await _httpRequestService.SendAsync<Result<CurrentHealthInsuranceCaseStateDto>>
            (HttpRequestSectionOptions.CsisHealthInsurance, httpRequest, cancellation);

        if ( httpResult.Succeeded == false ) {
            throw new BadRequestException(httpResult.Message + "\n" + httpResult.Response);
        }

        return httpResult.ApiResult.Data;
    }
}
