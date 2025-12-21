using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;
internal sealed partial class CsisWsmService
{
    // Added constants to detect successful relation/person findings inside cached log responses
    private const string LogIsPersonFoundTrue = "\"isPersonFound\":true";
    private const string LogIsRelationFoundTrue = "\"isRelationFound\":true";

    public async Task<ValidateParentChildRelationshipResponse> ValidateParentChildRelationship(ValidateParentChildRelationshipRequest request, CancellationToken cancellation) {
        var log = await _logRepo.GetOneAsync(
            x => x.Service == CsisWSMLog.ServiceEnum.ValidateParentChildRelationship
                 && x.Succeeded
                 && x.Request.Contains(request.ChildNationalCode)
                 && x.Request.Contains(request.ParentNationalCode)
                 && x.Response.Contains(LogIsPersonFoundTrue)
                 && x.Response.Contains(LogIsRelationFoundTrue),
            cancellationToken: cancellation);

        if ( log != null ) {
            var result = Deserialize<CsisWsmResponse<ValidateParentChildRelationshipResponse>>(log.Response).Response;
            return result;
        }

        HttpRequestResult<CsisWsmResponse<ValidateParentChildRelationshipResponse>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/sabte-ahval/family-relation-hoviat-inquiry";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<ValidateParentChildRelationshipResponse>>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new CsisWSMLog(
                    request.ChildNationalCode,
                    ServiceEnum.ValidateParentChildRelationship,
                    false,
                    request.ToJson(),
                    httpResult.StatusCode.ToString());

                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new CsisWSMLog(
                    request.ChildNationalCode,
                    ServiceEnum.ValidateParentChildRelationship,
                    httpResult.ApiResult.Extra.IsSuccess,
                    request.ToJson(),
                    httpResult.ApiResult.ToJson());

                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new BadRequestException();
            }

        } catch ( Exception ) {
            throw new CommandValidationException("در حال حاضر امکان دریافت اطلاعات از ثبت احوال وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Response;
    }
}
