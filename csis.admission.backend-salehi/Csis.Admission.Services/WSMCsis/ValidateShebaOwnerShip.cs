using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<ValidateShebaOwnershipResponse> ValidateShebaOwnerShip(string nationalCode, string accountNumber, CancellationToken cancellation) {

        var log = await _logRepo.GetOneAsync(x =>
        x.NationalCode == nationalCode
        && x.Service == ServiceEnum.ValidateShebaOwnership
        && x.Succeeded == true
        && x.Request.Contains(accountNumber.ToString())
        && x.Response.Contains(accountNumber.ToString()));

        if ( log != null ) {
            var result = Deserialize<CsisWsmApiResponse<ValidateShebaOwnershipResponse>>(log.Response).Data;

            return result;
        }

        HttpRequestResult<CsisWsmApiResponse<ValidateShebaOwnershipResponse>> httpResult = null;

        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/bmi/validate-owner";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, new { accountNumber, nationalIdentifier = nationalCode });
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<ValidateShebaOwnershipResponse>>
                (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog(nationalCode: nationalCode, ServiceEnum.ValidateShebaOwnership, false,
                    new { accountNumber }.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog(nationalCode: nationalCode, ServiceEnum.ValidateShebaOwnership, true,
                    new { accountNumber, nationalIdentifier = nationalCode }.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {

            throw new CommandValidationException("در حال حاضر امکان اعتبارسنجی شبا وجود ندارد. لطفا دقایقی دیگر تلاش نمایید.");
        }

        return httpResult.ApiResult.Data;
    }
}
