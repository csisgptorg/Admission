using Csis.Utilities.Extensions;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using static Csis.Admission.Domain.Entities.CsisWSMLog;

namespace Csis.Admission.Services;
internal sealed partial class CsisWsmService
{
    /// <summary>دریافت اطلاعات طلبه در مراکز حوزوی</summary>
    public async Task<string> GetStudentInfoInMarakezHowzavi(StudentInfoInMarakezHowzaviRequest request, CancellationToken cancellation) {

        _ = int.TryParse(request.Codm, out var codm);
        var log = await _logRepo.GetOneAsync(x =>
            x.Codm == codm &&
            x.NationalCode == request.NationalCode &&
            x.YektaCode == request.YektaCode &&
            x.ApprovalCenter == request.ApprovalCenter &&
            x.CaseNumberInApprovalCenter == request.CaseNumberInApprovalCenter &&
            x.DataGroup == request.DataGroup &&
            x.Service == ServiceEnum.GetStudentInfoInMarakezHowzavi &&
            x.Succeeded == true && x.CreatedOn > DateTime.Now.AddDays(-2));
        if ( log != null ) {
            return log.Response;
        }


        HttpRequestResult<string> httpResult = null;
        try {
            var token = await GetTokenApi(cancellation);
            var path = "/api/sp/GetMarakezFullData";
            var httpRequest = HttpRequestService.CreateRequest(HttpMethod.Post, path, request);
            httpResult = await _httpRequestService.SendAsync<string>
                (HttpRequestSectionOptions.CsisWsmApi, httpRequest, cancellation, token);

            if ( httpResult.Succeeded == false ) {
                log = new Domain.Entities.CsisWSMLog {
                    Codm = codm,
                    NationalCode = request.NationalCode,
                    YektaCode = request.YektaCode,
                    ApprovalCenter = request.ApprovalCenter,
                    CaseNumberInApprovalCenter = request.CaseNumberInApprovalCenter,
                    DataGroup = request.DataGroup,
                    Service = ServiceEnum.GetStudentInfoInMarakezHowzavi,
                    Succeeded = false,
                    Request = request.ToJson(),
                    Response = httpResult.StatusCode.ToString()
                };
                await _logRepo.InsertAsync(log, true, cancellation);

            } else {
                log = new Domain.Entities.CsisWSMLog {
                    Codm = codm,
                    NationalCode = request.NationalCode,
                    YektaCode = request.YektaCode,
                    ApprovalCenter = request.ApprovalCenter,
                    CaseNumberInApprovalCenter = request.CaseNumberInApprovalCenter,
                    DataGroup = request.DataGroup,
                    Service = ServiceEnum.GetStudentInfoInMarakezHowzavi,
                    Succeeded = httpResult.Succeeded,
                    Request = request.ToJson(),
                    Response = httpResult.ApiResult
                };
                await _logRepo.InsertAsync(log, true, cancellation);
            }

            if ( httpResult.Succeeded == false ) {
                throw new Exception();
            }

        } catch ( Exception ) {

            throw new BadRequestException("در حال حاضر امکان دریافت اطلاعات از مرکز مدیریت وجود ندارد. خواهشمند است در زمان دیگری اقدام فرمایید.");
        }

        return httpResult.ApiResult;
    }
}
