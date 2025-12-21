using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Enums;
using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<ValidateSpousalRelationshipResponse.Result> ValidateSpousalRelationship(ValidateSpousalRelationshipRequest request, CancellationToken cancellation) {

        var log = await _logRepo.GetOneAsync(x => x.Codm == request.Codm && x.NationalCode == request.NationalCodeSpouse
                                                                         && x.Service == ((request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Divorce) ? ServiceEnum.ValidateSpousalDivorceRelationship : ServiceEnum.ValidateSpousalMarriageRelationship)
                                                                         && x.Succeeded == true
                                                                         && x.Response.Contains(LogIsPersonFoundTrue)
                                                                         && x.Response.Contains(LogIsRelationFoundTrue)
                                                                         && x.Request.Contains(request.NationalCode)
                                                                         && x.Request.Contains(request.NationalCodeSpouse)
                                                                         && x.Request.Contains(((int) request.RelationType).ToString()), cancellationToken: cancellation);
        if ( log != null ) {
            return Deserialize<CsisWsmResponse<ValidateSpousalRelationshipResponse>>(log.Response).Response.GetResult();
        }

        HttpRequestResult<CsisWsmResponse<ValidateSpousalRelationshipResponse>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/sabte-ahval/relation-full-inquiry";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<ValidateSpousalRelationshipResponse>>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new CsisWSMLog(request.Codm, request.NationalCodeSpouse, request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Marriage ? ServiceEnum.ValidateSpousalMarriageRelationship : ServiceEnum.ValidateSpousalDivorceRelationship, false,
                request.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new CsisWSMLog(request.Codm, request.NationalCodeSpouse, request.RelationType == ValidateSpousalRelationshipRequest.RelationTypeEnum.Marriage ? ServiceEnum.ValidateSpousalMarriageRelationship : ServiceEnum.ValidateSpousalDivorceRelationship, httpResult.ApiResult.Extra.IsSuccess,
                request.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {

            }

        } catch ( Exception ) {
            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات از ثبت احوال وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Response.GetResult();
    }
}
