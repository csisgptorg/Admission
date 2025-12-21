using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<object> GetTajmieiSummary(string nationalCode, CancellationToken cancellation) {

        var log = await _logRepo.GetOneAsync(x => x.NationalCode == nationalCode && x.Service == ServiceEnum.GetTajmieiSummary && x.Succeeded == true);
        if ( log != null ) {
            var result = Deserialize<CsisWsmResponse<object>>(log.Response).Response;
            return result ?? throw new CommandValidationException("در حال حاضر امکان دریافت اطلاعات وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        HttpRequestResult<CsisWsmResponse<object>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/summary/tajmiei";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, new { nationalCode });
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<object>>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog(-1, nationalCode, ServiceEnum.GetTajmieiSummary, false,
                httpRequest.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog(-1, nationalCode, ServiceEnum.GetTajmieiSummary, httpResult.ApiResult.Extra.IsSuccess,
                httpRequest.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {

            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات مالک شماره موبایل از شاهکار وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Response;

    }
}
