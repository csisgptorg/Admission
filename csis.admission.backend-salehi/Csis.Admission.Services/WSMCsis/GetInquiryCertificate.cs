using Csis.Utilities.Extensions;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;

internal sealed partial class CsisWsmService
{
    public async Task<ResponseInquiryCertificateDto[]> GetInquiryCertificate(InquiryCertificateModel request, CancellationToken cancellation) {

        #region log
        var log = await _logRepo.GetOneAsync(x => x.Codm == request.Codm && request.DependentId==request.DependentId && x.Service == ServiceEnum.AcademicRecordsFromMinistry && x.Succeeded == true &&
            x.CreatedOn > DateTime.Now.AddDays(-2) && x.Request.Contains(request.NationalCode), cancellationToken: cancellation);

        ResponseInquiryCertificateDto[] result = null;
        if ( log != null ) {
            var response = Deserialize<CsisWsmApiResponse<ResponseInquiryCertificateData[]>>(log.Response).Data;
            result = response.Select(x=>new ResponseInquiryCertificateDto(x)).ToArray();
            return result;
        }
        #endregion

        HttpRequestResult<CsisWsmApiResponse<ResponseInquiryCertificateData[]>> httpResult = null;

        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/msrt/proxy/inquiry-by-trace-code";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<CsisWsmApiResponse<ResponseInquiryCertificateData[]>>
                (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog(request.Codm, request.NationalCode, ServiceEnum.AcademicRecordsFromMinistry, false,
                request.ToJson(), httpResult.StatusCode.ToString());
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog(request.Codm, request.NationalCode, ServiceEnum.AcademicRecordsFromMinistry, httpResult.Succeeded,
                request.ToJson(), httpResult.ApiResult.ToJson());
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {

            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات تحصیلی از وزارت علوم وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        result = httpResult.ApiResult.Data.Select(x => new ResponseInquiryCertificateDto(x)).ToArray();
        return result;
    }
}

