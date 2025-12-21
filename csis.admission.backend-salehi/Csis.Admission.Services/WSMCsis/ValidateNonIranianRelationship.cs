using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using System.Net;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    /// <summary>اعتبارسنجی نسبت غیرایرانی</summary>
    public async Task<ValidateNonIranianRelationshipResponse> ValidateNonIranianRelationship(ValidateNonIranianRelationshipRequest request, CancellationToken cancellation) {
        var log = await _logRepo.GetOneAsync(
            x => x.Service == CsisWSMLog.ServiceEnum.ValidateNonIranianRelationship
                 && x.Request.Contains(request.PersonYektaCode)
                 && x.Request.Contains(request.RelatedYektaCode)
                 && x.Response.Contains(LogIsRelationFoundTrue)
                 && x.Succeeded,
            cancellationToken: cancellation);

        if ( log != null ) {
            var result = Deserialize<CsisWsmApiResponse<ValidateNonIranianRelationshipResponse>>(log.Response);
            return result.Data;
        }

        HttpRequestResult<CsisWsmApiResponse<ValidateNonIranianRelationshipResponse>> httpResult = null;
        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/almostafa/person/get-relation";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<ValidateNonIranianRelationshipResponse>>(HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = CreateWithYektaCode(-1,
                    request.PersonYektaCode,
                    ServiceEnum.ValidateNonIranianRelationship,
                    false,
                    request.ToJson(),
                    httpResult.StatusCode.ToString());

                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = CreateWithYektaCode(-1,
                    request.PersonYektaCode,
                    ServiceEnum.ValidateNonIranianRelationship,
                    true, //TODO
                    request.ToJson(),
                    httpResult.ApiResult.ToJson());

                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {
            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات المصطفی وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Data;
    }
}

