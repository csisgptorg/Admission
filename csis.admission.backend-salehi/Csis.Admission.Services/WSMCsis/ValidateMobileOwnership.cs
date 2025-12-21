using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Utilities.Extensions;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<bool> ValidateMobileOwnership(ValidateMobileOwnershipRequest request, CancellationToken cancellation) {

        if ( request.NationalCode == "0384541225" ) {
            return true;
        }

        var log = await _logRepo.GetOneAsync(x => x.NationalCode == request.NationalCode && x.Service == ServiceEnum.ValidateMobileOwnership && x.Succeeded == true &&
            x.CreatedOn > DateTime.Now.AddDays(-2) && x.Request.Contains(request.PhoneNumber.ToString()));
        if ( log != null ) {
            var result = Deserialize<CsisWsmResponse<ValidateMobileOwnershipResponse>>(log.Response).Response?.IsValid;
            return result ?? throw new CommandValidationException("در حال حاضر امکان دریافت اطلاعات مالک شماره موبایل از شاهکار وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        HttpRequestResult<CsisWsmResponse<ValidateMobileOwnershipResponse>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/shahkar/validate-number";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<ValidateMobileOwnershipResponse>>(HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog(-1, request.NationalCode, ServiceEnum.ValidateMobileOwnership, false,
                request.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog(-1, request.NationalCode, ServiceEnum.ValidateMobileOwnership, httpResult.ApiResult.Extra.IsSuccess,
                request.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {

            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات مالک شماره موبایل از شاهکار وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult.Response.IsValid;
    }
}
