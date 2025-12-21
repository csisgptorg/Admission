using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    /// <summary>اعتبارسنجی نسبت غیرایرانی</summary>
    public async Task<GetIdentityInfoByYektaCodeResponse> GetIdentityInfoByYektaCode(string yektaCod, CancellationToken cancellation) {
        var log = await _logRepo.GetOneAsync(
            x => x.Service == ServiceEnum.AlmostafaPersonHoviat
                 && x.Request.Contains(yektaCod)
                 && x.Response.Contains(LogIsRelationFoundTrue)
                 && x.Succeeded,
            cancellationToken: cancellation);

        if ( log != null ) {
            var result = Deserialize<CsisWsmApiResponse<GetIdentityInfoByYektaCodeResponse>>(log.Response);
            return result.Data;
        }

        HttpRequestResult<CsisWsmApiResponse<GetIdentityInfoByYektaCodeResponse>> httpResult = null;
        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/almostafa/person/hoviat";
            var request = new GetIdentityInfoByYektaCodeRequest(yektaCod);
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<GetIdentityInfoByYektaCodeResponse>>
                (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = CreateWithYektaCode(-1,
                    request.YektaCode,
                    ServiceEnum.AlmostafaPersonHoviat,
                    false,
                    request.ToJson(),
                    httpResult.StatusCode.ToString());

                await _logRepo.InsertAsync(log, true, cancellation);

                if ( httpResult.Response.Contains("یافت نشد") ) {
                    throw new CommandValidationException("کد یکتا واردشده در سامانه المصطفی یافت نشد. لطفاً صحت اطلاعات را بررسی کنید.");
                }

            } else {
                log = CreateWithYektaCode(-1,
                    request.YektaCode,
                    ServiceEnum.AlmostafaPersonHoviat,
                    true, //TODO
                    request.ToJson(),
                    httpResult.ApiResult.ToJson());

                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( CommandValidationException ex ) {
            throw new BadRequestException(ex.Message);
        } 
        catch ( Exception) {
            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات المصطفی وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Data;
    }
}
