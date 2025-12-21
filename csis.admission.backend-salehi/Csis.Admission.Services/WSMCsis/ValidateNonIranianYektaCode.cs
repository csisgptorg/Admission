using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<ValidateNonIranianYektaCodeResponse> ValidateNonIranianYektaCode(int codm, string yektaCode, CancellationToken cancellation) {
        var log = await _logRepo.GetOneAsync(x => x.Succeeded && x.YektaCode == yektaCode && x.Request.Contains(yektaCode) && x.Service == ServiceEnum.ValidateNonIranianYektaCode, cancellationToken: cancellation);
        if ( log != null ) {
            var result = Deserialize<CsisWsmApiResponse<ValidateNonIranianYektaCodeResponse>>(log.Response);
            return result.Data;
        }

        HttpRequestResult<CsisWsmApiResponse<ValidateNonIranianYektaCodeResponse>> httpResult = null;
        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/atba/member-info";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, new { SearchType = 4, SearchCode = yektaCode });
            _logger.LogInformation("Sending ValidateNonIranianYektaCode request to {Path} with YektaCode: {YektaCode},{httpRequest}", path, yektaCode, httpRequest.ToJson());
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<ValidateNonIranianYektaCodeResponse>>
                (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = CreateWithYektaCode(codm, yektaCode, ServiceEnum.ValidateNonIranianYektaCode, false,
                new { SearchType = 4, SearchCode = yektaCode }.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = CreateWithYektaCode(codm, yektaCode, ServiceEnum.ValidateNonIranianYektaCode, httpResult.Succeeded,
                new { SearchType = 4, SearchCode = yektaCode }.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {
            throw new CommandValidationException("اطلاعات وارد شده برای کد یکتا معتبر نمی باشد. لطفا صحت اطلاعات را بررسی نمایید");
        }

        return httpResult.ApiResult.Data;
    }
}

