using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;
internal sealed partial class CsisWsmService
{
    public async Task<bool> ValidateSibaAccountNumber(ValidateSibaAccountNumberRequest request, CancellationToken cancellation) {

        var log = await _logRepo.GetOneAsync(x => x.NationalCode == request.NationalIdentifier && x.Service == ServiceEnum.ValidateMobileOwnership && x.Succeeded == true &&
            x.CreatedOn > DateTime.Now.AddDays(-2) && x.Request.Contains(request.AccountNumber.ToString()));
        if ( log != null ) {
            return Deserialize<CsisWsmResponse<ValidateSibaAccountNumberResponse>>(log.Response).Response.IsMatched;
        }

        HttpRequestResult<CsisWsmResponse<ValidateSibaAccountNumberResponse>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/dolat-network/validate-sheba";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<ValidateSibaAccountNumberResponse>>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new CsisWSMLog(request.Codm, request.NationalIdentifier, ServiceEnum.ValidateMobileOwnership, false,
                 request.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new CsisWSMLog(request.Codm, request.NationalIdentifier, ServiceEnum.ValidateMobileOwnership, httpResult.ApiResult.Extra.IsSuccess,
                request.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new BadRequestException();
            }

        } catch ( Exception ) {
            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات بانکی وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Response.IsMatched;
    }
}
