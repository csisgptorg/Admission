using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<GetIranianImageFromSabteAhvalResponse> GetIranianImageFromSabteAhval(string nationalCode, string birthDate, CancellationToken cancellation) {
        var log = await _logRepo.GetOneAsync(
           x => x.Service == ServiceEnum.GetIranianImageFromSabteAhval
                && x.Request.Contains(nationalCode)
                && x.Request.Contains(birthDate)
                && x.CreatedOn < DateTime.Now.AddDays(-2)
                && x.Succeeded,
           cancellationToken: cancellation);

        if ( log != null ) {
            var result = Deserialize<CsisWsmApiResponse<GetIranianImageFromSabteAhvalResponse>>(log.Response);
            return result.Data;
        }

        HttpRequestResult<CsisWsmApiResponse<GetIranianImageFromSabteAhvalResponse>> httpResult = null;
        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/sabte-ahval/inquiry/image";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, new { nationalCode, birthDate });
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<GetIranianImageFromSabteAhvalResponse>>(HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog(-1,
                    nationalCode,
                    ServiceEnum.GetIranianImageFromSabteAhval,
                    false,
                    new { nationalCode, birthDate }.ToJson(),
                    httpResult.StatusCode.ToString());

                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog(-1,
                    nationalCode,
                    ServiceEnum.GetIranianImageFromSabteAhval,
                    true,
                    new { nationalCode, birthDate }.ToJson(),
                    httpResult.ApiResult.ToJson());

                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {
            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات ثبت احوال وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Data;
    }
}
