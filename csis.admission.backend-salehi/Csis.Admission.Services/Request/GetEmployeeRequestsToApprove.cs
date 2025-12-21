using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Domain.Enums;
using Csis.Paging;
using Csis.Utilities.Extensions;

namespace Csis.Admission.Services;

/// <summary>دریافت درخواست های نیازمند تایید کارمند</summary>
internal sealed partial class RequestService : IRequestService
{
    public async Task<IPagedList<SearchPersonnelRequestsToApproveResult>> GetEmployeeRequestsToApprove
        (GetEmployeeRequestsToApproveQuery query, CancellationToken cancellationToken) {

        var employee = await CurrentEmployee();

        var requestsToApproves = await repo.SearchPagedAsync<SearchPersonnelRequestsToApproveResult>(
            query.SearchFilters, x => x.ApprovalStatus == ApprovalStatus.Pending
            && (
                   (employee.IsSenior == true && (x.NextFlowApprover == RequestApprovalFlow.Employee || x.NextFlowApprover == RequestApprovalFlow.SeniorEmployee))
                || (employee.IsSenior == false && x.NextFlowApprover == RequestApprovalFlow.Employee)
            ) &&
            (
                    (query.Codm.HasValue && x.Codm == query.Codm.Value) ||
                    !query.Codm.HasValue
            ),
            query.PageIndex, query.PageSize, query.SortBy.HasValue() ? query.SortBy : "-Id", cancellationToken: cancellationToken);

        var codms = requestsToApproves.Select(x => x.Codm).ToArray();
        var students = await studentRepo.GetAllAsync(x => codms.Contains(x.Codm), cancellationToken: cancellationToken);

        foreach ( var resquest in requestsToApproves ) {
            resquest.CaseIsActive = students.Single(x => x.Codm == resquest.Codm).IsActive;
        }

        // تبدیل رشته JSON به مدل مشخص شده در jsonPayloadModel و تنظیم اطلاعات فایل
        await FileInfoHelper.SetRequestFilesInfoAsync([.. requestsToApproves], fileManagementService);
        foreach ( var request in requestsToApproves ) {
            request.PayloadModelObject = System.Text.Json.JsonSerializer.Deserialize<object>(request.Payload);
        }

        return requestsToApproves;
    }
}

