using Csis.Abstractions.Results;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Services;

internal sealed partial class CsisSupInsuranceService : ICsisSupInsuranceService
{
    private readonly IHttpRequestService _httpRequestService;
    public CsisSupInsuranceService(IHttpRequestService httpRequestService) {
        _httpRequestService = httpRequestService;
    }

    public async Task<CurrentSupInsuranceCaseStateDto> GetHealthStatus(int codm, long? dependentId, CancellationToken cancellation) {
        var path = "/api/reception-api/get-health-status/" + codm;
        if ( dependentId > 0 ) {
            path += "?takafolId=" + dependentId;
        }

        var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Get, path);
        var httpResult = 
            await _httpRequestService.SendAsync<Result<CurrentSupInsuranceCaseStateDto>>(HttpRequestSectionOptions.CsisSupInsurance, httpRequest, cancellation);

        if ( httpResult.Succeeded == false ) {
            throw new BadRequestException(httpResult.Message + "\n" + httpResult.Response);
        }

        return httpResult.ApiResult.Data;
    }

    public async Task<CurrentSupInsuranceCaseStateDto> GetLifeStatus(int codm, long? dependentId, CancellationToken cancellation) {

        var path = "api/reception-api/get-life-status/" + codm;
        if ( dependentId > 0 ) {
            path += "?takafolId=" + dependentId;
        }

        var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Get, path);
        var httpResult = 
            await _httpRequestService.SendAsync<Result<CurrentSupInsuranceCaseStateDto>>(HttpRequestSectionOptions.CsisSupInsurance, httpRequest, cancellation);

        if ( httpResult.Succeeded == false ) {
            throw new BadRequestException(httpResult.Message + "\n" + httpResult.Response);
        }

        return httpResult.ApiResult.Data;
    }
}
