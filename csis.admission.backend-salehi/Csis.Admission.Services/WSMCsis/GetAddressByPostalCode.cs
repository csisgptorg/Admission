using Csis.Utilities.Extensions;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;
internal sealed partial class CsisWsmService
{
    public async Task<GetAddressByPostalCodeResponse> GetAddressByPostalCode(int codm, long postalCode, CancellationToken cancellation) {

        //TODO remove after test
        var requiresDualStudentApprovalTest = false;
        if ( postalCode == 3739136644 ) {
            postalCode = 3739136643;
            requiresDualStudentApprovalTest = true;
        }

        var log = await _logRepo.GetOneAsync(x => x.Codm == codm && x.Service == ServiceEnum.GetAddressByPostalCode && x.Succeeded == true &&
            x.CreatedOn > DateTime.Now.AddDays(-2) && x.Request.Contains(postalCode.ToString()));
        if ( log != null ) {
            var result = Deserialize<CsisWsmResponse<GetAddressByPostalCodeResponse>>(log.Response).Response;
            if ( requiresDualStudentApprovalTest ) {
                result.Address.RequiresDualStudentApproval = true;
            }
            return result;
        }

        HttpRequestResult<CsisWsmResponse<GetAddressByPostalCodeResponse>> httpResult = null;
        try {
            var token = await GetToken(cancellation);
            var path = "/api/v1/dolat-network/inquiry-post";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, new { postalCode });
            httpResult = await _httpRequestService.SendAsync<CsisWsmResponse<GetAddressByPostalCodeResponse>>
                (HttpRequestSectionOptions.CsisWsm, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog(codm, null, ServiceEnum.GetAddressByPostalCode, false,
                    new { postalCode }.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog(codm, null, ServiceEnum.GetAddressByPostalCode, httpResult.ApiResult.Extra.IsSuccess,
                    new { postalCode }.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();   
            }

        } catch ( Exception ) {

            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات پستی وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }
        

        var respons = httpResult.ApiResult.Response;
        respons.Address.RequiresDualStudentApproval =
            await _studentRepo.IsDualStudentApprovalRequiredForAddress(new StudentAddressApprovalRequestPrc(respons.GetAddress(codm, postalCode)));
        if ( requiresDualStudentApprovalTest ) {
            respons.Address.RequiresDualStudentApproval = true;
        }

        return respons;
    }
}
