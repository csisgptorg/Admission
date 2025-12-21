using Csis.Utilities.Extensions;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Domain.Entities;
using static Csis.Admission.Domain.Entities.CsisWSMLog;
using System.Net;

namespace Csis.Admission.Services;
internal sealed partial class CsisWsmService
{
    public async Task<GetIdentityInfoByNationalCodeResponse> GetIdentityInfoByNationalCode(GetIdentityInfoByNationalCodeRequest request, CancellationToken cancellation) {

        var log = await _logRepo.GetOneAsync(x => x.Codm == request.Codm
                                                  && x.Service == ServiceEnum.GetIdentityInfoByNationalCode
                                                  && x.Succeeded == true
                                                  && x.CreatedOn > DateTime.Now.AddDays(-2)
                                                  && x.Request.Contains(request.NationalCode)
                                                  && x.Response.Contains(request.BirthDate.IntDateToString())
                                                  && x.Response.Contains(request.NationalCode), cancellationToken: cancellation);
        if ( log != null ) {
            var result = Deserialize<CsisWsmResponse<GetIdentityInfoByNationalCodeResponse>>(log.Response).Response;
            return result;
        }

        HttpRequestResult<CsisWsmResponse<GetIdentityInfoByNationalCodeResponse>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/sabte-ahval/hoviat-full-inquiry";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<GetIdentityInfoByNationalCodeResponse>>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new CsisWSMLog(request.Codm.Value, request.NationalCode, ServiceEnum.GetIdentityInfoByNationalCode, false,
                request.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new CsisWSMLog(request.Codm.Value, request.NationalCode, ServiceEnum.GetIdentityInfoByNationalCode, httpResult.ApiResult.Extra.IsSuccess,
                request.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {

            throw new CommandValidationException("در حال حاضر امکان دریافت اطلاعات کد ملی از ثبت احوال وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        if ( httpResult.ApiResult.Response == null ) {
            var response = new GetIdentityInfoByNationalCodeResponse();
            httpResult.ApiResult.Response = response;
        }

        return httpResult.ApiResult.Response;
    }

    /// <summary>اعتبارسنجی کد ملی ApiM</summary>
    public async Task<GetIdentityInfoByNationalCodeResponse> GetIdentityInfoByNationalCode(GetIdentityInfoByNationalCodeRequestApiM request, CancellationToken cancellation) {

        request = request with { BirthDate = request.BirthDate.Replace("-", "/") };
        var log = await _logRepo.GetOneAsync(x => x.Service == ServiceEnum.GetIdentityInfoByNationalCodeWsmApi
                                                  && x.Succeeded
                                                  && x.CreatedOn > DateTime.Now.AddDays(-2)
                                                  && x.Request != null
                                                  && x.Request.Contains(request.NationalCode)
                                                  && x.Request.Contains(request.BirthDate)
                                                  && x.Response.Contains(request.NationalCode)
                                                  , cancellationToken: cancellation);
        if ( log != null ) {
            var result = Deserialize<CsisWsmApiResponse<GetIdentityInfoByNationalCodeResponse>>(log.Response);
            return result.Data ?? result.Response;
        }

        HttpRequestResult<CsisWsmApiResponse<GetIdentityInfoByNationalCodeResponse>> httpResult = null;
        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/sabte-ahval/hoviat-full-mixed";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<GetIdentityInfoByNationalCodeResponse>>
                (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new CsisWSMLog(-1, request.NationalCode, ServiceEnum.GetIdentityInfoByNationalCodeWsmApi, false,
                    request.ToJson(), httpResult.StatusCode.ToString());
            } else {
                log = new CsisWSMLog(-1, request.NationalCode, ServiceEnum.GetIdentityInfoByNationalCodeWsmApi, true,
                    request.ToJson(), httpResult.ApiResult.ToJson());
            }

            await _logRepo.InsertAsync(log, true, cancellation);

            if ( httpResult.Succeeded == false && httpResult.StatusCode!=HttpStatusCode.BadRequest) {
                throw new Exception();
            }
        } catch ( Exception ) {
            throw new CommandValidationException("در حال حاضر امکان دریافت اطلاعات ثبت احوال وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult?.Data ?? new GetIdentityInfoByNationalCodeResponse();
    }
}
